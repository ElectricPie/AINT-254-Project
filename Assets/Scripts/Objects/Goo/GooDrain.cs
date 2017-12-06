using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooDrain : MonoBehaviour {

    public float amount;

    public float speed = 0.1f;

    private bool m_draining;
    private bool m_filling;

    private bool m_drained;

    private GameObject m_goo;

    private Vector3 m_amount;

    private float m_counter;

    private void Start()
    {
        m_goo = transform.GetChild(0).gameObject;

        m_amount = new Vector3(0, amount, 0);
    }

    public void Update()
    {
        //Draining the goo
        if (m_draining && m_counter > 0)
        {
            Vector3 change = m_amount * Time.deltaTime;

            m_goo.transform.Translate(-change * speed);

            m_counter -= (amount * Time.deltaTime) * speed;
        }
        else if (m_draining && m_counter < 0)
        {
            m_draining = false;
            m_drained = true;
        }

        //Filling the goo
        if (m_filling && m_counter > 0)
        {
            Vector3 change = m_amount * Time.deltaTime;

            m_goo.transform.Translate(change * speed);

            m_counter -= (amount * Time.deltaTime) * speed;
        }
        else if (m_filling && m_counter < 0)
        {
            m_filling = false;
            m_drained = false;
        }
    }

    public void Toggle()
    {
        if (!m_draining && !m_filling)
        {
            if (m_drained) //Fills the goo
            {
                m_filling = true;
                m_counter = amount;
            }
            else //Drains the goo
            {
                m_draining = true;
                m_counter = amount;
            }
        }
    }
}
