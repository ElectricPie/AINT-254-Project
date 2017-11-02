using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMovement : MonoBehaviour {
    [SerializeField]
    public bool interactable = false;

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
            //Debug.Log("Velocity: " + m_velocityEffects[i]);
        }

        //Debug.Log("Temp: " + temp);
        GetComponent<Rigidbody>().velocity += temp;
    }

    public void UpdateWells(GameObject obj, Vector3 vec)
    {
        //Debug.Log("Debug: " + gameObject);
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

    public void PickUp()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Drop()
    {
        //GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void AjustPos(Transform pos)
    {
        //gameObject.transform.position = pos.transform.position + pos.forward * 3;
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3;
    }
}
