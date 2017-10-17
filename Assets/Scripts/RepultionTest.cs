using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepultionTest : MonoBehaviour {
    

    //public
    public bool repel;

    [Range(0, 1)]
    public float strength;

    //Private
    private List<Vector3> m_direcToObj = new List<Vector3>();

    private float m_radius;

    private List<GameObject> m_objects = new List<GameObject>();

	// Use this for initialization
	void Start () {
        m_radius = GetComponent<SphereCollider>().radius * 0.8f;
        //m_objects.Add(null);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < m_direcToObj.Count; i++)
        {
            m_direcToObj[i] = m_objects[i].transform.position - transform.position;
        }
    }

    void Repel()
    {
        //testObj.GetComponent<Rigidbody>().velocity = temp; //Applys the vector to the object
        //Debug.Log("Velocity: " + temp);
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(m_direcToObj[i].x, m_direcToObj[i].y, m_direcToObj[i].z) * strength; //Creates a vector in the direction of the well

            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    void Attract()
    {
        //testObj.GetComponent<Rigidbody>().velocity = temp;
        //Debug.Log("Velocity: " + temp);
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(-m_direcToObj[i].x, -m_direcToObj[i].y, -m_direcToObj[i].z) * strength;

            //Debug.Log("Obj: " + m_objects[i]);
            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Other: " + other.name);
        m_objects.Add(other.gameObject);
        m_direcToObj.Add(new Vector3());
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Collied: " + other.name);
        if (repel == true)
        {
            Repel();
        }
        else
        {
            Attract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Hit: " + other);
        m_objects[CheckObjList(other.gameObject)].GetComponent<ObjectMovement>().RemoveWell(gameObject);
        m_direcToObj.RemoveAt(CheckObjList(other.gameObject));
        m_objects.RemoveAt(CheckObjList(other.gameObject));
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
}
