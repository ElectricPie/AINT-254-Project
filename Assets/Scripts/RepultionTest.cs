using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepultionTest : MonoBehaviour {
    [Range (0,2)]
    public float strength;

    public GameObject testObj;
    public GameObject UI;

    public bool repel;

    private Vector3 m_direcToObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Get the direction of the other object
        m_direcToObj = testObj.transform.position - transform.position;

        UpdateUI();

        if (repel == true)
        {
            Repel();
        }
        else
        {
            Attract();
        }
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
        Vector3 temp = new Vector3(m_direcToObj.x * strength, m_direcToObj.y * strength, m_direcToObj.z * strength); //Creates a vector in the direction of the well

        //testObj.GetComponent<Rigidbody>().velocity = temp; //Applys the vector to the object
        //Debug.Log("Velocity: " + temp);
        testObj.GetComponent<ObjectMovement>().CheckEffect(gameObject, temp);
    }

    void Attract()
    {
        Vector3 temp = new Vector3(-m_direcToObj.x * strength, -m_direcToObj.y * strength, -m_direcToObj.z * strength);

        //testObj.GetComponent<Rigidbody>().velocity = temp;
        //Debug.Log("Velocity: " + temp);
        testObj.GetComponent<ObjectMovement>().CheckEffect(gameObject, temp);
    }
}
