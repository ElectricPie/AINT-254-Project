using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour {
    public GameObject dispensePrefab;

    private GameObject m_dispensedItem;

    private void Start()
    {
        m_dispensedItem = Instantiate(dispensePrefab);
        m_dispensedItem.transform.position = new Vector3(-23.4f, 4.65f, 30.8f);
    }

    public void Activate()
    {
        m_dispensedItem.SetActive(false);

        Invoke("Dispense", 1);
    }

    public void Dispense()
    {
        m_dispensedItem.transform.position = new Vector3(-23.4f, 4.65f, 30.8f);

        m_dispensedItem.SetActive(true);
    }
}
