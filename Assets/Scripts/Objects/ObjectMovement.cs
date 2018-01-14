using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectMovement : MonoBehaviour {
    public bool interactable = false;

    //Movement
    private List<GameObject> m_effectsOrig = new List<GameObject>();
    private List<Vector3> m_velocityEffects = new List<Vector3>();

    private GameObject m_aim;

    private bool m_pickedUp;
    

    // Use this for initialization
    void Start () {
        m_effectsOrig.Add(null);
        m_velocityEffects.Add(new Vector3());
        m_pickedUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_pickedUp == true)
        {
            Vector3 directTar = transform.localPosition - m_aim.transform.position;

            GetComponent<Rigidbody>().velocity = -directTar * 20;
        }
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
    public void PickUp(GameObject aim)
    {
        m_aim = aim;
        m_pickedUp = true;
    }

    public void Drop(Vector3 velocity)
    {
        //Stops all velocity on the objected when droped
        GetComponent<Rigidbody>().velocity = velocity;
        m_aim = null;
        m_pickedUp = false;
    }

    public void AjustPos(Transform pos)
    {
        //Transforms the position of the object to infornt of the camera
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3;
    }

    public bool isHeld
    {
        get { return m_pickedUp; }
    }
}
