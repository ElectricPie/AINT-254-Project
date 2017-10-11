using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {
    List<GameObject> m_effectsOrig = new List<GameObject>();
    List<Vector3> m_velocityEffects = new List<Vector3>();

    // Use this for initialization
    void Start () {
        m_effectsOrig.Add(null);
        m_velocityEffects.Add(new Vector3());
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 temp = new Vector3();

		for (int i = 0; i < m_velocityEffects.Count; i++)
        {
            temp += m_velocityEffects[i];
            //Debug.Log("Velocity: " + m_velocityEffects[i]);
        }

        //Debug.Log("Temp: " + temp);

        GetComponent<Rigidbody>().velocity = temp;
    }

    public void CheckEffect(GameObject gameObj, Vector3 vec)
    {
        
        //Debug.Log("Checking");

        bool found = false;
        int location = 0;

        for (int i = 0; i < m_effectsOrig.Count; i++)
        {
            if (m_effectsOrig[i] == gameObj)
            {
                //Debug.Log("Found");
                found = true;
                location = i;
            }
        }
        //Debug.Log("Found: " + found);

        
        if (found == true)
        {
            m_velocityEffects[location] = vec;
            //Debug.Log("Added vec" + m_velocityEffects[location]);
        }
        else
        {
            //Debug.Log("Creating");
            m_effectsOrig.Add(gameObj);
            //Debug.Log("Obj: " + m_effectsOrig[1]);
            m_velocityEffects.Add(new Vector3());
            //Debug.Log("Obj: " + m_velocityEffects[1]);
        }

    }
}
