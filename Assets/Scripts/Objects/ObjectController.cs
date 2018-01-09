using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //public
    public float wellStrength = 0.5f;

    //Private
    private List<GameObject> m_objects;
    private List<GameObject> m_gravityWells;

    private float timer;

    // Use this for initialization
    void Start()
    {
        m_objects = new List<GameObject>(); //Instantiates new object list
        m_gravityWells = new List<GameObject>(); //Instantiates new gravity well list
    }

    private void Update()
    {
        if (timer > 0.1)
        {
            DistanceCheck();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void DistanceCheck()
    {
        for (int i = 0; i < m_gravityWells.Count; i++) //Loops for the number of gravity wells
        {
            for (int j = 0; j < m_objects.Count; j++) //Loops for the number of objects
            {
                float dist = Vector3.Distance(m_gravityWells[i].transform.position, m_objects[j].transform.position); //Gets the distance of the object from the gravity well

                if (dist < 3)
                {
                    m_gravityWells[i].GetComponent<GravityWell>().AffectObject(m_objects[j]); //Tells the gravity well to affect an object
                }
                
            }
        }
    }


    public GameObject NewObject
    {
        set { m_objects.Add(value); } //Adds a new object to the list
    }

    public GameObject NewWell
    {
        set { m_gravityWells.Add(value); } //Adds a new object to the list
    }

    public List<GameObject> GetList
    {
        get { return m_objects; } //Returnts the entire list;
    }

    public float GetWellStrength
    {
        get { return wellStrength; }
    }
}
