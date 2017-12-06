using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public GameObject pressIcon;
    public GameObject pickUpIcon;
    public GameObject eraserIcon;

    public GameOver dead;

    public Transform checkpoint;

    [SerializeField]
    private float m_health = 5;

    [SerializeField]
    private float m_speed = 5.0f;

    [SerializeField]
    private float m_jump = 1.0f;

    private Rigidbody m_rigidbody;

    private bool m_grounded;
    private bool m_holdingObj = false;
    private bool m_stance = false;

    private ObjectMovement m_heldObj;

    Vector3 temp;

    // Use this for initialization
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pressIcon.SetActive(false);
        pickUpIcon.SetActive(false);
        eraserIcon.SetActive(false);

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
            string tag = hit.collider.tag;

            if (tag == "Object")
            {
                pickUpIcon.SetActive(true);
            }
            else if (tag == "PlyBtn")
            {
                pressIcon.SetActive(true);
            }
            else if (tag == "GravityWell")
            {
                eraserIcon.SetActive(true);
            }
            else
            {
                pressIcon.SetActive(false);
                pickUpIcon.SetActive(false);
                eraserIcon.SetActive(false);
            }

            //Health
            if (m_health <= 0)
            {
                
            }
        }

        //Interacting
        if (Input.GetButtonDown("Interact"))
        {
            //Checks if already holding an object
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
                    if (hit.transform.tag == "Object")
                    {
                        try
                        {
                            m_heldObj = hit.collider.gameObject.GetComponent<ObjectMovement>();
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

        //Jumping
        if (Input.GetButtonDown("Jump") && m_grounded == true)
        {
            Vector3 temp = new Vector3();
            temp.y += m_jump;
            m_grounded = false;

            m_rigidbody.velocity += temp;
        }

        transform.Translate(xInput, 0, zInput);
        transform.Rotate(0, xMouseInput, 0);

        //Change Stance
        if (Input.GetButtonDown("Stance"))
        {
            if (m_stance)
            {
                tag = "Untagged";

                m_stance = !m_stance;
            }
            else
            {
                tag = "Player";

                m_stance = !m_stance;
            }
        }

        if (m_holdingObj)
        {
            m_heldObj.AjustPos(gameObject.transform);
            pickUpIcon.SetActive(false);
        }

        //Escape
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Quitting");
            Debug.Break();
            Application.Quit();
        }

        if (m_health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_grounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        m_grounded = false;
    }

    private void Death()
    {
        transform.position = checkpoint.position;
        m_health = 5;
    }

    public float Health
    {
        get { return m_health;  }
        set { m_health += value; }
    }
}

