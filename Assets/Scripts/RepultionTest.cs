using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepultionTest : MonoBehaviour {
    [Range (0,1)]
    public float strength;

    public GameObject testObj;
    public GameObject UI;

    public bool repel;

    private Vector3 m_direcToObj;

    private float m_radius;

	// Use this for initialization
	void Start () {
        m_radius = GetComponent<SphereCollider>().radius * 0.8f;
    }
	
	// Update is called once per frame
	void Update () {
        //Get the direction of the other object
        m_direcToObj = testObj.transform.position - transform.position;

        UpdateUI();

        
    }

    void UpdateUI()
    {
        //Updates the UI to show the direction
        UI.transform.GetChild(0).GetComponent<Text>().text = m_direcToObj.x.ToString();
        UI.transform.GetChild(1).GetComponent<Text>().text = m_direcToObj.y.ToString();
        UI.transform.GetChild(2).GetComponent<Text>().text = m_direcToObj.z.ToString();
    }

    void Repel()
    {
        Vector3 temp = new Vector3(m_direcToObj.x, m_direcToObj.y, m_direcToObj.z) * strength; //Creates a vector in the direction of the well

        //testObj.GetComponent<Rigidbody>().velocity = temp; //Applys the vector to the object
        //Debug.Log("Velocity: " + temp);
        testObj.GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
    }

    void Attract()
    {
        Vector3 temp = new Vector3(-m_direcToObj.x, -m_direcToObj.y, -m_direcToObj.z) * strength;

        //testObj.GetComponent<Rigidbody>().velocity = temp;
        //Debug.Log("Velocity: " + temp);
        testObj.GetComponent<ObjectMovement>().UpdateWells(gameObject, temp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
    }

    private void OnTriggerStay()
    {
        Debug.Log("Collied: " + name);
        if (repel == true)
        {
            Repel();
        }
        else
        {
            Attract();
        }

        GetDistance();
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Hit: " + other);
        testObj.GetComponent<ObjectMovement>().RemoveWell(gameObject);
    }

    private void GetDistance()
    {
        float 
    }
}
