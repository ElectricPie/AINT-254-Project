﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {
    public GameObject dispensePrefab;
    public GameObject cubeSpwan;

    private GameObject m_dispensedItem;

    private void Start()
    {
        m_dispensedItem = Instantiate(dispensePrefab);
        //m_dispensedItem.transform.localPosition = new Vector3(0f, -0.795f, 0f);
        m_dispensedItem.transform.localPosition = gameObject.transform.position;
    }

    public void Activate()
    {
        m_dispensedItem.SetActive(false);

        Invoke("Dispense", 1);
    }

    public void Dispense()
    {
        //m_dispensedItem.transform.localPosition = new Vector3(0f, -0.795f, 0f);
        m_dispensedItem.transform.localPosition = gameObject.transform.position;

        m_dispensedItem.SetActive(true);
    }
}
