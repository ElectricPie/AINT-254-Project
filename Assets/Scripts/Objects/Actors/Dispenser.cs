using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {
    //Public
    public GameObject dispensePrefab;
    public GameObject cubeSpwan;

    //Private
    private GameObject m_dispensedItem;

    private void Start()
    {
        m_dispensedItem = Instantiate(dispensePrefab);
        //m_dispensedItem.transform.localPosition = new Vector3(0f, -0.795f, 0f);
        m_dispensedItem.transform.localPosition = gameObject.transform.position;
    }

    public void Activate()
    {
        m_dispensedItem.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
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
