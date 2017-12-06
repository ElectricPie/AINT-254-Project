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

    //Private Vairables
    [SerializeField]
    private bool m_disabled;
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
            m_gravityWells[i].GetComponent<RepultionTest>().Index = i;
            //m_gravityWells[i].SetActive(false); //Disables gravity wells

            m_activeWells[i] = true;
        }

        m_wellCycle = 0; //Sets the cycle for which gravity well is to be moved next
        m_strength = intialStrength; //Sets the starting strength of gravity wells 
        m_power = 1; //Sets the starting number used on the UI to show power
        m_disabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Controls
        //-Gravity Well Previews       
        if (!m_disabled)
        {
            if (Input.GetButton("Fire1"))
            {
                m_polarity = false;

                if (RaycastTag("Wall") | RaycastTag("Ground") || RaycastTag("Button"))
                {
                    gravityWellPreview.SetActive(true);

                    gravityWellPreview.transform.position = RaycastPoint();
                }
            }

            if (Input.GetButton("Fire2"))
            {
                m_polarity = true;

                if (RaycastTag("Wall") || RaycastTag("Ground") || RaycastTag("Button"))
                {
                    gravityWellPreview.SetActive(true);

                    gravityWellPreview.transform.position = RaycastPoint();
                }
            }

            //-Fire Gravity Well
            if (Input.GetButtonUp("Fire1"))
            {
                Vector3 impact = RaycastPoint();

                ChooseWell(impact, true);
            }

            if (Input.GetButtonUp("Fire2"))
            {
                Vector3 impact = RaycastPoint();

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

    private Vector3 RaycastPoint()
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "Ground" || hit.collider.tag == "Button") 
            {
                return hit.point;
            }
        }
        else
        {
            return new Vector3();
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
        else
        {
            return null;
        }

        return null;
    }

    private bool RaycastTag(string tag)
    {
        //Creates a raycast from the camera to a position infront of the camera
        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward * 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag)
            {
                return true;
            }
        }
        else
        {
            return false;
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
        RepultionTest adjustingWell = m_gravityWells[wellIndex].GetComponent<RepultionTest>();

        m_gravityWells[wellIndex].SetActive(true); //Activates the well incase it has become deactive
        m_activeWells[wellIndex] = true;


        adjustingWell.Polarity = polarity; //Changes the polarity of the well to the desired type
        adjustingWell.Strength = m_strength; //Adjusts the strength of the well
        adjustingWell.UpdateColour();

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
        int index = temp.GetComponent<RepultionTest>().Index;
        m_gravityWells[index].SetActive(false);
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
}
