using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjButton : Button
{
    //Public
    public GameObject target;

    //private Button btnController;

    //Private
    private GameObject m_button;
    private bool m_active;
    private float m_buttonOrigin;

	// Use this for initialization
	void Start () {
        //btnController = GameObject.Find("BtnController").GetComponent<Button>();
        m_button = transform.GetChild(1).gameObject;
        m_buttonOrigin = m_button.transform.localPosition.y;
        Debug.Log("Pos: " + m_buttonOrigin);
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Tag: " + other.gameObject.tag);

        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Player")
        {
            m_button.transform.localPosition = new Vector3(0, m_buttonOrigin -0.01f, 0);

            m_active = true;

            TriggerOn(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_active)
        {
            m_button.transform.localPosition = new Vector3(0, m_buttonOrigin, 0);
        }

        TriggerOff(target);

        m_active = false;
    }

}
