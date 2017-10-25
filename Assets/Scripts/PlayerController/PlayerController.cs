using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.8f;

    [SerializeField]
    private float m_jump = 5;

    private float m_interactDelay;

    private Rigidbody m_rigidbody;

    private bool m_grounded;
    private bool m_holdingObj = false;

    private ObjectMovement m_heldObj;

    Vector3 temp;

    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        float xMouseInput = Input.GetAxis("Mouse X");

        temp = (transform.right * xInput + transform.forward * zInput) * m_speed;

        if (temp.x > 0.2)
            temp.x = 0.2f;

        if (temp.x < -0.2)
            temp.x = -0.2f;

        if (temp.z > 0.2)
            temp.z = 0.2f;

        if (temp.z < -0.2)
            temp.z = -0.2f;

        //Jumping
        if (Input.GetAxis("Jump") == 1 && m_grounded == true)
        {
            temp.y += m_jump;
            m_grounded = false;
        }

        //Interacting
        if (Input.GetAxis("Interact") == 1 && m_interactDelay <= 0)
        {
            if (m_holdingObj)
            {
                m_holdingObj = false;

                m_heldObj.Drop();
                m_heldObj = null;

                m_interactDelay = 1;            }
            else
            {
                Transform cam = Camera.main.transform;
                Ray ray = new Ray(cam.position, cam.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider);

                    try
                    {
                        m_heldObj = hit.collider.gameObject.GetComponent<ObjectMovement>();
                        m_heldObj.PickUp();
                        m_holdingObj = true;
                    }
                    catch
                    {
                        m_holdingObj = false;
                    }
                }

                m_interactDelay = 1;
            }
        }


        m_rigidbody.velocity += temp;

        if (m_holdingObj)
        {
            Debug.Log("Moveing Obj");
            m_heldObj.AjustPos(gameObject.transform);
        }

        if (m_interactDelay > 0)
        {
            m_interactDelay -= Time.deltaTime * 1;
        }

        transform.Rotate(0, xMouseInput, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_grounded = true;
    }
}

