using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {
    private ObjectController m_objCont;
    private int m_polarity = -1; //1 is attraction || -1 is repultion

    private GameObject m_repultionParticals;
    private GameObject m_attractionParticals;

    private int m_wellIndex = -1;

    // Use this for initialization
    void Start () {

        m_objCont = GameObject.Find("ObjectController").GetComponent<ObjectController>();

        m_objCont.NewWell = gameObject; //Adds the well to the list

        m_repultionParticals = transform.GetChild(1).gameObject; //Gets the GameObject for the repultion particals
        m_attractionParticals = transform.GetChild(2).gameObject; //Gets the GameObject for the attraction particals
    }

    public void AffectObject(GameObject obj)
    {
        Vector3 directObj = gameObject.transform.position - obj.transform.position;
        //Debug.Log("Direction: " + directObj);
        //Debug.Log("Strenght: " + m_objCont.GetWellStrength);

        Vector3 objVel = obj.GetComponent<Rigidbody>().velocity; //Gets the objects current velocity

        Vector3 newVel = objVel; //Create a new temp vector3 to store the new calculated velocity of the object

        if (newVel.x < 2 || newVel.x > -2) //Limits the objects velocity in the x axis
        {
            newVel.x += directObj.x * m_objCont.GetWellStrength * m_polarity; //gets the direction of the cube from the gravity well and 
                                                                              //multiplys it by the wells strenght and the polarity
        }

        if (newVel.y < 2 || newVel.y > -2) //Limits the objects velocity in the y axis
        {
            newVel.y += directObj.y * m_objCont.GetWellStrength * m_polarity; //gets the direction of the cube from the gravity well and 
                                                                              //multiplys it by the wells strenght and the polarity
        }

        if (newVel.z < 2 || newVel.y > -2) //Limits the objects velocity in the z axis
        {
            newVel.z += directObj.z * m_objCont.GetWellStrength * m_polarity; //gets the direction of the cube from the gravity well and 
                                                                              //multiplys it by the wells strenght and the polarity
        }

        //Debug.Log("New Velocity: " + newVel);
        obj.GetComponent<Rigidbody>().velocity = newVel; //Applys the new velocity to the object
    }

    //Toggles the state of the gravity wells partical effects
    private void ToggleParticals(bool state)
    {
        m_attractionParticals.SetActive(!state);
        m_repultionParticals.SetActive(state);
    }


    public bool Polarity
    {
        set
        {
            if (value)
            {
                m_polarity = -1; //Sets the polarity to repell objects
                ToggleParticals(true);
            }
            else
            {
                m_polarity = 1; //Sets the polarity to attract objects
                ToggleParticals(false);
            }
        }

        get
        {
            if (m_polarity == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public int WellIndex
    {
        set
        {
            if (value >= 0)
                m_wellIndex = value;
        }
        get { return m_wellIndex; }
    }
}
