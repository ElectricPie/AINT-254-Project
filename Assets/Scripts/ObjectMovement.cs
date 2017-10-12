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

    public void UpdateWells(GameObject obj, Vector3 vec)
    {
        int location = CheckWells(obj);

        if (location != 0)
        {
            m_velocityEffects[location] = vec;
            //Debug.Log("Added vec" + m_velocityEffects[location]);
        }
        else
        {
            //Debug.Log("Creating");
            m_effectsOrig.Add(obj);
            //Debug.Log("Obj: " + m_effectsOrig[1]);
            m_velocityEffects.Add(new Vector3());
            //Debug.Log("Obj: " + m_velocityEffects[1]);
        }
    }

    public void RemoveWell(GameObject obj)
    {
        int location = CheckWells(obj);

        m_effectsOrig.RemoveAt(location);
        m_velocityEffects.RemoveAt(location);
    }

    private int CheckWells(GameObject obj)
    {
        int location = 0;
 
        for (int i = 0; i < m_effectsOrig.Count; i++)
        {
            if (m_effectsOrig[i] == obj)
            {
                //Debug.Log("Found");
                location = i;
            }
        }

        return location;
    }
}
