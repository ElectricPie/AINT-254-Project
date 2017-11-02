using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public GameObject pressIcon;
    public GameObject pickUpIcon;

    [SerializeField]
    private float m_speed = 5.0f;

    [SerializeField]
    private float m_jump = 1.0f;

    private Rigidbody m_rigidbody;

    private bool m_grounded;
    private bool m_holdingObj = false;

    private ObjectMovement m_heldObj;

    Vector3 temp;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pressIcon.SetActive(false);
        pickUpIcon.SetActive(false);

        m_rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        float xInput = Input.GetAxis("Horizontal") * Time.deltaTime * m_speed;
        float zInput = Input.GetAxis("Vertical") * Time.deltaTime * m_speed;
        float xMouseInput = Input.GetAxis("Mouse X");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Transform cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        //-Icons
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Object")
            {
                pickUpIcon.SetActive(true);
            }
            else if (hit.collider.tag == "PlyBtn")
            {
                pressIcon.SetActive(true);
            }
            else
            {
                pressIcon.SetActive(false);
                pickUpIcon.SetActive(false);
            }
        }

        //Interacting
        if (Input.GetButtonDown("Interact"))// && m_interactDelay <= 0)
        {
            if (m_holdingObj)
            {
                m_holdingObj = false;

                m_heldObj.Drop();
                m_heldObj = null;
            }
            else
            {
                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.collider);
                    if (hit.transform.tag == "Object")
                    {
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
                    else if (hit.transform.tag == "PlyBtn")
                    {
                        hit.transform.gameObject.GetComponent<PlyButton>().TriggerBtn();
                    }
                }


            }
        }

        if (m_holdingObj)
        {
            m_heldObj.AjustPos(gameObject.transform);
        }

        //Jumping
        if (Input.GetAxis("Jump") == 1 && m_grounded == true)
        {
            Vector3 temp = new Vector3();
            temp.y += m_jump;
            m_grounded = false;

            m_rigidbody.velocity += temp;
        }

        transform.Translate(xInput, 0, zInput);
        transform.Rotate(0, xMouseInput, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_grounded = true;
    }
}

