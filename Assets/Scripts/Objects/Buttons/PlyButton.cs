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
        if (toggle)
        {
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
}
