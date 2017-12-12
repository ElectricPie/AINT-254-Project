using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    //Public Vairables
    public int wellAmount = 3;
    public float intialStrength = 0.5f;

    public Renderer polarityIndicator;

    public GameObject strengthIndicator;
    public GameObject gravityWell;
    public GameObject gravityWellPreview;

    public Transform gravityWellHolder;

    public List<string> wellableSurfaces;

    //Private Vairables
    [SerializeField]
    private bool m_disabled;
    private bool m_paused;
    private bool m_polarity; //true: attraction      false: repultion

    private float m_strength;

    private int m_power;
    private int m_wellCycle;

    private bool[] m_activeWells;

    private GameObject[] m_gravityWells;

    // Use this for initialization
    void Start () {
        m_gravityWells = new GameObject[wellAmount]; //Creates an array to hold gravity wells
        m_activeWells = new bool[wellAmount];
            
        gravityWellPreview = Instantiate(gravityWellPreview); //Instatiates and holds the gameobject of the aiming gravity well

        //Instatiates and adds gravity wells to the array
        for (int i = 0; i < m_gravityWells.Length; i++)
        {
            m_gravityWells[i] = Instantiate(gravityWell, gravityWellHolder);
            m_gravityWells[i].name = "GravityWell " + i; //Renames gravity wells for easyer identification
            m_gravityWells[i].GetComponent<GravityWell>().WellIndex = i; //Sets the index of the well for easy identification
            //m_gravityWells[i].SetActive(false); //Disables gravity wells

            m_activeWells[i] = true;
        }

        m_wellCycle = 0; //Sets the cycle for which gravity well is to be moved next
        m_strength = intialStrength; //Sets the starting strength of gravity wells 
        m_power = 1; //Sets the starting number used on the UI to show power
        m_disabled = false; //Ensures the gravity well gun is operational at start
    }
	
	// Update is called once per frame
	void Update () {
        //Controls
        if (!m_paused)
        {
            //-Gravity Well Previews    
            if (Input.GetButton("Fire1"))
            {
                m_polarity = false;
                
                if (RaycastTag(wellableSurfaces))//RaycastTag("Wall","Ground","Button")) //| RaycastTag("Ground") || RaycastTag("Button"))
                {
                    gravityWellPreview.SetActive(true);

                    gravityWellPreview.transform.position = RaycastPoint(wellableSurfaces);
                }
            }

            if (Input.GetButton("Fire2"))
            {
                m_polarity = true;

                if (RaycastTag("Wall") || RaycastTag("Ground") || RaycastTag("Button"))
                {
                    gravityWellPreview.SetActive(true);

                    gravityWellPreview.transform.position = RaycastPoint(wellableSurfaces);
                }
            }

            //-Fire Gravity Well
            if (Input.GetButtonUp("Fire1"))
            {
                Vector3 impact = RaycastPoint(wellableSurfaces);

                ChooseWell(impact, true);
            }

            if (Input.GetButtonUp("Fire2"))
            {
                Vector3 impact = RaycastPoint(wellableSurfaces);

                ChooseWell(impact, false);
            }

            //Clearing gravity wells
            if (Input.GetButtonDown("Clear"))
            {
                if (RaycastTag("GravityWell"))
                {
                    GameObject temp = RaycastGameobject("GravityWell");

                    if (temp != null)
                    {
                        ClearWell(temp);
                    }
                }
            }
        }
    }

    private Vector3 RaycastPoint(List<string> tags)
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (hit.collider.tag == tags[i])
                {
                    return hit.point;
                }
            }
        }

        return new Vector3();
    }

    private GameObject RaycastGameobject(string tag)
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    
    //Raycast tags
    private bool RaycastTag(string tag) //For single inputs
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag) //Checks the objects tag vs the string provided
            {
                return true;
            }
        }

        return false;
    }

    private bool RaycastTag(params string[] tags) //For custom inputs
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        for (int i = 0; i < tags.Length; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == tag) //Checks the objects tag vs the parameters provided
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool RaycastTag(List<string> tags) //For pre set lists
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        for (int i = 0; i < tags.Count; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == tags[i]) //Checks the objects tag vs the list provided
                {
                    return true;
                }
            }
        }

        return false;
    }



    //Ajust the vairables of the next well the the aimed location
    private void ChooseWell(Vector3 impact, bool polarity)
    {
        bool[] activeWells = CheckActiveWells();
        int inactiveWell = -1;

        for (int i = 0; i < activeWells.Length; i++)
        {
            if (activeWells[i] == false)
            {
                inactiveWell = i;
            }
        }

         
        if (inactiveWell == -1)
        {
            MoveWell(m_wellCycle, impact, polarity);

            //Increments the well cycle so that the a diffrent well is fired
            if (m_wellCycle < wellAmount - 1) //Increases the well cycle by 1 if its not above the initial well amount
            {
                m_wellCycle++;
            }
            else //Sets the well cycle to 0 if it goes above 2
            {
                m_wellCycle = 0;
            }
        }
        else
        {
            MoveWell(inactiveWell, impact, polarity);
        }

        gravityWellPreview.SetActive(false);
    }

    private void MoveWell(int wellIndex, Vector3 impact, bool polarity)
    {
        //RepultionTest adjustingWell = m_gravityWells[wellIndex].GetComponent<RepultionTest>();

        GravityWell adjustingWell = m_gravityWells[wellIndex].GetComponent<GravityWell>();

        m_gravityWells[wellIndex].SetActive(true); //Activates the well incase it has become deactive
        m_activeWells[wellIndex] = true;

        adjustingWell.Polarity = polarity;

        /*
        adjustingWell.Polarity = polarity; //Changes the polarity of the well to the desired type
        adjustingWell.Strength = m_strength; //Adjusts the strength of the well
        adjustingWell.UpdateColour();
        */

        m_gravityWells[wellIndex].transform.position = impact; //Moves the well to the inpact location of the raycast
    }

    private bool[] CheckActiveWells()
    {
        bool[] activeWells = new bool[wellAmount];

        for (int i = 0; i < m_gravityWells.Length; i++)
        {
            activeWells[i] = m_gravityWells[i].activeSelf;
        }

        return activeWells;
    }


    public void ClearWell(GameObject temp) //Clears the well
    {
        Debug.Log("Clearing Well");
        m_gravityWells[temp.GetComponent<GravityWell>().WellIndex].SetActive(false);
    }


    //GET SET
    public int Power
    {
        get { return m_power; }
    }

    public bool Polarity
    {
        get { return m_polarity; }
    }

    public bool Disable
    {
        set { m_disabled = value; }
    }

    public int WellCycle
    {
        get { return m_wellCycle; }
    }

    public bool Paused
    {
        set { m_paused = value; }
    }
}
