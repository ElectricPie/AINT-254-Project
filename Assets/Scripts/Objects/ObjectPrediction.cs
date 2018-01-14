using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPrediction : MonoBehaviour {

    public GameObject projectionPrefab;

    private bool m_projecting;
    private GameObject m_projection;
    private ObjectController m_objCont;
    private Vector3 m_lastPos;
    private bool isMoving;

    private void Start()
    {
        m_objCont = GameObject.Find("ObjectController").GetComponent<ObjectController>();

        m_projecting = false;
        isMoving = true;
    }

    private void Update()
    {
        CheckIfMoving();
    }

    public void Projection()
    {
        if (!isMoving && !m_projecting && GetComponent<ObjectMovement>().isHeld)
        {
            m_projecting = true;

            //Creates a projections cube
            m_projection = Instantiate(projectionPrefab);

            //Prevents projected cube from colliding with atual cube
            Physics.IgnoreCollision(GetComponent<Collider>(), m_projection.GetComponent<Collider>());

            //Puts projected cube in cubes position
            m_projection.transform.position = transform.position;
            m_projection.transform.rotation = transform.rotation;

        }
        else if (isMoving && m_projecting)
        {
            RemoveProjection();
        }
        else if (!GetComponent<ObjectMovement>().isHeld)
        {
                RemoveProjection();
        }
    }

    private void CheckIfMoving()
    {
        Vector3 curPos = transform.position;
        
        if (curPos == m_lastPos)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        m_lastPos = curPos;
    }

    public void RemoveProjection()
    {
        m_objCont.RemoveObject(m_projection);
        Destroy(m_projection);
        m_projecting = false;
    }

    public bool Projecting
    {
        get { return m_projecting; }
    }
}
