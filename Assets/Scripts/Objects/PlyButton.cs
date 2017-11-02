using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyButton : Button {

    public bool toggle;

    public GameObject target;

    private bool m_toggled = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerBtn()
    {
        //Debug.Log("Pushed PlyBtn");

        if (toggle)
        {
            Debug.Log("Toggle Mode");
            Debug.Log(m_toggled);
            if (m_toggled)
            {
                TriggerOff(target);
                m_toggled = !m_toggled;
            }
            else
            {
                TriggerOn(target);
                m_toggled = !m_toggled;
            }
        }
        else
        {
            TriggerOn(target);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tag: " + other.gameObject.tag);

        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Player")
        {
            transform.position += new Vector3(0, -0.05f, 0);

            active = true;

            TriggerOn(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            transform.localPosition = new Vector3(0, 0.3f, 0);
        }

        TriggerOff(target);

        active = false;
    }
    */
}
