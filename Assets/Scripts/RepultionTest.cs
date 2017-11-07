﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepultionTest : MonoBehaviour {


    //public
    public Material repelMat;
    public Material attractMat;

    private bool m_polarity;

    [Range(0, 1), SerializeField]
    private float m_strength;

    //Private
    private List<Vector3> m_direcToObj = new List<Vector3>();

    private float m_radius;

    private List<GameObject> m_objects = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //Sets the radius to be the same as the collider
        m_radius = GetComponent<SphereCollider>().radius * 0.8f;
        //m_objects.Add(null);

        //Sets the colour of the well to show its polarity
        if (m_polarity)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        //Sets the outer sphere of the well to show the well polarity
        Material temp;

        if (m_polarity)
        {
            temp = repelMat;
        }
        else
        {
            temp = attractMat;
        }

        transform.GetChild(0).GetComponent<Renderer>().material = temp;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Updates all objects that the gravity well has effect over
        for (int i = 0; i < m_direcToObj.Count; i++)
        {
            m_direcToObj[i] = m_objects[i].transform.position - transform.position;
        }
    }

    private void Update()
    {
        

        //Debug.Log("Index: " + index);
    }

    void Repel()
    {
        //testObj.GetComponent<Rigidbody>().velocity = temp; //Applys the vector to the object
        //Debug.Log("Velocity: " + temp);
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(m_direcToObj[i].x, m_direcToObj[i].y, m_direcToObj[i].z) * m_strength; //Creates a vector in the direction of the well

            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    void Attract()
    {
        //testObj.GetComponent<Rigidbody>().velocity = temp;
        //Debug.Log("Velocity: " + temp);
        for (int i = 0; i < m_objects.Count; i++)
        {
            Vector3 temp = new Vector3(-m_direcToObj[i].x, -m_direcToObj[i].y, -m_direcToObj[i].z) * m_strength;

            //Debug.Log("Obj: " + m_objects[i]);
            m_objects[i].GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Object" || other.tag == "Player")
        {
            //Debug.Log("Other: " + other.name);

            if (CheckObjList(other.gameObject) >= 0)
            {
                Debug.Log("Object already affected");
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
        Debug.Log("Object leaving");

        if (other.tag == "Object" || other.tag == "Player")
        {
            //Debug.Log("Hit: " + other);
            m_objects[CheckObjList(other.gameObject)].GetComponent<ObjectMovement>().RemoveWell(gameObject);

            int index = CheckObjList(other.gameObject);
            //Debug.Log("Index: " + index);

            m_direcToObj.RemoveAt(CheckObjList(other.gameObject));
            m_objects.RemoveAt(CheckObjList(other.gameObject));
        }
    }

    private int CheckObjList(GameObject obj)
    {
        //Debug.Log("Gameobject: " + obj);

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

    private void OnDisable()
    {
        //Debug.Log("Disable");
        m_objects.Clear();
        m_direcToObj.Clear();
    }

    public void Polarity(bool polarity)
    {
        m_polarity = polarity;
    }

    public void Strength(float strength)
    {
        m_strength = strength;
    }
}
