using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMovement : MonoBehaviour {
    public bool interactable = false;

    

    //Movement
    private List<GameObject> m_effectsOrig = new List<GameObject>();
    private List<Vector3> m_velocityEffects = new List<Vector3>();

    

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
        }

        //Applys velocity to the object
        GetComponent<Rigidbody>().velocity += temp;
    }

    public void UpdateWells(GameObject obj, Vector3 vec)
    {
        int location = CheckWells(obj);

        if (location != 0)
        {
            m_velocityEffects[location] = vec;
        }
        else
        {
            m_effectsOrig.Add(obj);
            m_velocityEffects.Add(new Vector3());
        }
    }

    public void RemoveWell(GameObject gravityWell)
    {
        int location = CheckWells(gravityWell);
        //Debug.Log("Index: " + location);

        m_effectsOrig.RemoveAt(location);
        m_velocityEffects.RemoveAt(location);
    }


    private int CheckWells(GameObject gravityWell)
    {
        int location = 0;
 
        for (int i = 0; i < m_effectsOrig.Count; i++)
        {
            if (m_effectsOrig[i] == gravityWell)
            {
                location = i;
            }
        }

        return location;
    }

    public void Drop()
    {
        //Stops all velocity on the objected when droped
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void AjustPos(Transform pos)
    {
        //Transforms the position of the object to infornt of the camera
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3;
    }
}
