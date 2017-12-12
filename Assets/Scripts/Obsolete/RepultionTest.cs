using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepultionTest : MonoBehaviour {
    //public
    public Material repelMat;
    public Material attractMat;
    [SerializeField]
    private bool m_polarity; 
    private int m_index;

    [Range(0, 1), SerializeField]
    private float m_strength;

    //Private
    private List<Vector3> m_direcToObj = new List<Vector3>();

    private List<GameObject> m_objects = new List<GameObject>();

    private GameObject m_repultionParticals;
    private GameObject m_attractionParticals;

	// Use this for initialization
	void Start () {
        //Sets the outer sphere of the well to show the well polarity
        UpdateColour();

        m_repultionParticals = transform.GetChild(1).gameObject;
        m_attractionParticals = transform.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Updates all objects that the gravity well has effect over
        for (int i = 0; i < m_direcToObj.Count; i++)
        {
            m_direcToObj[i] = m_objects[i].transform.position - transform.position;
        }
    }

    void Repel()
    {
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(m_direcToObj[i].x, m_direcToObj[i].y, m_direcToObj[i].z) * m_strength; //Creates a vector in the direction of the well

            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    void Attract()
    {
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(-m_direcToObj[i].x, -m_direcToObj[i].y, -m_direcToObj[i].z) * m_strength;

            //Debug.Log("Obj: " + m_objects[i]);
            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    private int CheckObjList(GameObject obj)
    {
        int location = -1;

        for (int i = 0; i < m_objects.Count; i++)
        {
            if (m_objects[i] == obj)
            {
                location = i;
            }
        }

        return location;
    }

    public void UpdateColour()
    {
        Material temp;

        if (m_polarity)
        {
            temp = repelMat;
        }
        else
        {
            temp = attractMat;
        }

        GetComponent<Renderer>().material = temp;
        transform.GetChild(0).GetComponent<Renderer>().material = temp;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Well Trigger: " + other.tag);
        if (other.tag == "Object" || other.tag == "Player")
        {
            if (CheckObjList(other.gameObject) >= 0)
            {
                //Debug.Log("Object already affected");
            }
            else
            {
                m_objects.Add(other.gameObject);
                m_direcToObj.Add(new Vector3());
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag == "Object" || other.tag == "Player"))
        {
            //Debug.Log("Collied: " + other.name);
            if (m_polarity == true)
            {
                Repel();
            }
            else
            {
                Attract();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Object leaving");

        if (other.tag == "Object" || other.tag == "Player")
        {
            m_objects[CheckObjList(other.gameObject)].GetComponent<ObjectMovement>().RemoveWell(gameObject);

            int index = CheckObjList(other.gameObject);

            m_direcToObj.RemoveAt(index);
            m_objects.RemoveAt(index);
        }
    }

    //GET SET
    public bool Polarity
    {
        set
        {
            m_polarity = value;
            if (m_polarity)
            {
                m_repultionParticals.SetActive(true);
                m_attractionParticals.SetActive(false);
            }
            else
            {
                m_repultionParticals.SetActive(false);
                m_attractionParticals.SetActive(true);
            }
        }
    }

    public float Strength
    {
        set { m_strength = value; }
    }

    public int Index
    {
        get { return m_index; }
        set { m_index = value; }
    }
}
