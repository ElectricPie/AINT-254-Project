using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public int wellAmount = 3;

    public Renderer polarityIndicator;

    public GameObject strengthIndicator;
    public GameObject gravityWell;
    public GameObject gravityWellPreview;

    public Transform gravityWellHolder;

    private bool m_polarity;
    private bool m_cleared;

    private float m_strength;

    private int m_power;
    private int m_wellCycle;

    //private List<GameObject> m_gravityWells;
    private GameObject[] m_gravityWells;
    // Use this for initialization
    void Start () {
        m_gravityWells = new GameObject[wellAmount];
        gravityWellPreview = Instantiate(gravityWellPreview);
        m_cleared = true;

        for (int i = 0; i < m_gravityWells.Length; i++)
        {
            m_gravityWells[i] = Instantiate(gravityWell, gravityWellHolder);
            m_gravityWells[i].SetActive(false);
        }

        m_wellCycle = 0;
        m_strength = 0.2f;
        m_power = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //Controls
        //-Polarity
        if (Input.GetButtonDown("Toggle"))
        {
            //Polarity Indicator
            if (m_polarity)
            {
                polarityIndicator.material.color = Color.red;
                m_polarity = !m_polarity;
            }
            else
            {
                polarityIndicator.material.color = Color.blue;
                m_polarity = !m_polarity;
            }
        }


        //-Power
        if (Input.GetButtonDown("Increase"))
        {
            if (m_strength < 0.9) {
                m_strength += 0.1f;
                m_power += 1;
            }

            UpdateStrengthIndi();
        }

        if (Input.GetButtonDown("Decrease"))
        {
            if (m_strength > 0.2f)
            {
                m_strength -= 0.1f;
                m_power -= 1;
            }

            UpdateStrengthIndi();
        }

        
        //-Landing Preview
        if (Input.GetButton("Fire"))
        {
            Transform cam = Camera.main.transform;
            Ray ray = new Ray(cam.position, cam.forward * 20);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider);
                //Debug.Log(hit.collider.tag);

                if (hit.collider.tag == "Wall" || hit.collider.tag == "Ground")
                {
                    gravityWellPreview.SetActive(true);

                    gravityWellPreview.transform.position = hit.point;
                }

            }
        }
        

        //-Fire
        if (Input.GetButtonUp("Fire"))
        {
            gravityWellPreview.SetActive(false);

            Transform cam = Camera.main.transform;
            Ray ray = new Ray(cam.position, cam.forward * 20);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider);
                //Debug.Log(hit.collider.tag);
                

                if (hit.collider.tag == "Wall" || hit.collider.tag == "Ground")
                {

                    m_gravityWells[m_wellCycle].SetActive(true);

                    m_gravityWells[m_wellCycle].GetComponent<RepultionTest>().Polarity(m_polarity);
                    m_gravityWells[m_wellCycle].GetComponent<RepultionTest>().Strength(m_strength);

                    m_gravityWells[m_wellCycle].transform.position = hit.point;

                    if (m_wellCycle < wellAmount - 1)
                    {
                        m_wellCycle++;
                    }
                    else
                    {
                        m_wellCycle = 0;
                    }

                    m_cleared = false;
                }
            }
            
        }

        //-Clear
        if (Input.GetButtonDown("Clear") && !m_cleared) {
            if (m_wellCycle > 1)
            {
                m_wellCycle--;
            }
            else
            {
                m_wellCycle = wellAmount - 1;
            }

            m_gravityWells[m_wellCycle].SetActive(false);
            m_cleared = true;
        }
    }

    private void UpdateStrengthIndi()
    {
        Animator anim = strengthIndicator.GetComponent<Animator>();

        anim.SetInteger("power", m_power);
    }

    public int Power
    {
        get { return m_power; }
    }

    public bool Polarity
    {
        get { return m_polarity; }
    }

    public int WellCycle
    {
        get { return m_wellCycle; }
    }
}
