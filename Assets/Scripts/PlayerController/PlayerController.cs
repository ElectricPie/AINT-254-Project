using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public
    public GameObject pressIcon;
    public GameObject pickUpIcon;
    public GameObject eraserIcon;

    public GameMenuUI menuUI;

    public GameOver dead;

    public Transform checkpoint;

    public WellIndicatorUI indicatorUI;

    //Private
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

    private bool m_paused;

    private ObjectMovement m_heldObj;

    Vector3 temp;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pressIcon.SetActive(false);
        pickUpIcon.SetActive(false);
        eraserIcon.SetActive(false);

        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (!m_paused)
        {
            //Movement Inputs
            float xInput = Input.GetAxis("Horizontal") * Time.deltaTime * m_speed;
            float zInput = Input.GetAxis("Vertical") * Time.deltaTime * m_speed;

            //Camera Inputs
            float xMouseInput = Input.GetAxis("Mouse X");
            float yInput = -Input.GetAxis("Mouse Y");

            int wellIndex = -1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Transform cam = Camera.main.transform;
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;

            //-Icons/UI
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
                    wellIndex = hit.collider.GetComponent<GravityWell>().WellIndex;
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

                indicatorUI.UpdateLook(wellIndex);
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
                Debug.Log("obj name: " + m_heldObj.name);
                m_heldObj.AjustPos(gameObject.transform);
                pickUpIcon.SetActive(false);
            }


            if (m_health <= 0)
            {
                Death();
            }

            transform.Translate(xInput, 0, zInput); //Moves the player in a a direction

            transform.Rotate(0, xMouseInput, 0); //Rotates the player horizontaly based om input
            transform.GetChild(0).Rotate(yInput, 0, 0); //Rotates the camera verticaly based on input
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_grounded = true;
        }
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
        get { return m_health; }
        set { m_health += value; }
    }

    public bool Paused
    {
        set { m_paused = value; }
    }
}
