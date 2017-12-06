using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrediction : MonoBehaviour {

    public GameObject projectionPrefab;
    public int steps;
    public float gravity = 9.8f;

    private GameObject[] m_projections;
    private Vector3 m_cubePos;
    private Rigidbody m_rigidbody;

    private void Start()
    {
        /*
        m_projections = new GameObject[steps];

        for (int i = 0; i < m_projections.Length; i++)
        {
            GameObject newObj = Instantiate(projectionPrefab);
            newObj.SetActive(false);
            m_projections[i] = newObj;
        }
        */
    }

    private void Projection(Vector3 dir)
    {
        float force = 1f;

        float Vx = dir.x * force;
        float Vy = dir.y * force;
        float Vz = dir.z * force;

        for (int i = 0; i < m_projections.Length; i++)
        {
            float t = i * 0.1f;
            m_projections[i].transform.position = new Vector3(transform.position.x + Vx * t, 
                                                             (transform.position.y + Vy * t) - (gravity * t * t / 2.0f), 
                                                             transform.position.z + Vz * t);
            m_projections[i].SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (other.tag == "GravityWell")
        {
            Vector3 dirFromWell = (other.gameObject.transform.position - transform.position).normalized;

            Projection(dirFromWell);
        }
        */
    }
}
