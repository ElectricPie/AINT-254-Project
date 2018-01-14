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
    public GameObject objectAim;

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

            RaycastHit[] hits;
            hits = Physics.RaycastAll(cam.position, cam.forward, 4.0F);

            bool[] icons = new bool[3];
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i] = false;
            }


            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    string tag = hits[i].transform.tag;

                    if (tag == "Object")
                    {
                        pickUpIcon.SetActive(true); //Enables pick up icon
                        icons[0] = true;
                    }
                    else if (tag == "PlyBtn")
                    {
                        pressIcon.SetActive(true);//Enables interaction icon
                        icons[1] = true;
                    }
                    else if (tag == "GravityWell")
                    {
                        eraserIcon.SetActive(true); //Enables clear well icon
                        icons[2] = true;
                        wellIndex = hits[i].collider.GetComponent<GravityWell>().WellIndex;
                    }
                }
            }

            //Clears all icons if player is not looking at anything that can be interacted with
            if (CheckAllFalse(icons))
            {
                //Hides all icons
                pressIcon.SetActive(false);
                pickUpIcon.SetActive(false);
                eraserIcon.SetActive(false);
            }



            //Interacting
            if (Input.GetButtonDown("Interact"))
            {
                //Checks if already holding an object
                if (m_holdingObj)
                {
                    int dropMod = 40;

                    m_holdingObj = false;

                    float dir = transform.rotation.eulerAngles.y;
                    Vector3 dropVel;                 

                    if (dir >= 315 || dir < 45)
                    {
                        Debug.Log("Forward");
                        dropVel = new Vector3(xInput * dropMod, m_rigidbody.velocity.y, zInput * dropMod);
                    }
                    else if (dir >= 45 && dir < 135)
                    {
                        Debug.Log("Right");
                        dropVel = new Vector3(zInput * dropMod, m_rigidbody.velocity.y, xInput  * dropMod);
                    }
                    else if (dir >= 135  && dir < 225)
                    {
                        Debug.Log("Back");
                        dropVel = new Vector3(-xInput * dropMod, m_rigidbody.velocity.y, -zInput * dropMod);
                    }
                    else  if (dir  >= 255 && dir < 315)
                    {
                        Debug.Log("Left");
                        dropVel = new Vector3(-zInput * dropMod, m_rigidbody.velocity.y, xInput * dropMod);
                    }
                    else
                    {
                        dropVel = new Vector3();
                    }

                    m_heldObj.Drop(dropVel);
                    m_heldObj = null;
                }
                else
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        if (hits[i].transform.tag == "Object")
                        {
                            m_heldObj = hits[i].collider.gameObject.GetComponent<ObjectMovement>();
                            m_heldObj.PickUp(objectAim);
                            m_holdingObj = true;
                        }
                        else if (hits[i].transform.tag == "PlyBtn")
                        {
                            hits[i].transform.gameObject.GetComponent<PlyButton>().TriggerBtn();
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
                //m_heldObj.AjustPos(gameObject.transform);
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

    private bool CheckAllFalse(bool[] array)
    {
        for (int i = 0; i < array.Length; i ++)
        {
            if (array[i])
            {
                return false;
            }
        }

        return true;
    }

    //Collision
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
