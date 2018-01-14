using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //Public
    public GameObject RHinge;
    public GameObject LHinge;

    public GameObject cubeTest;

    public Text debugText;

    //Private
    private float m_counter;
    private float m_startTimer;

    private bool m_openHatch = false;

    // Use this for initialization
    void Start()
    {
        m_startTimer = 1;
        m_openHatch = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if 
        Vector3 cubVel = cubeTest.GetComponent<Rigidbody>().velocity;
        float cubSum = cubVel.x + cubVel.y + cubVel.z; //Calculates if the object is at rest

        if (m_startTimer < 0) //Stops the hatches immediately opening
            if (cubeTest.transform.position.y > -1)
            {
                if (cubSum == 0f)
                {
                    m_openHatch = true;
                }
            }
            else
            {
                Debug.Log("Below Threshold");
                m_openHatch = false; 
            }
        else
        {
            m_startTimer -= 1 * Time.deltaTime;
        }


        if (m_openHatch == true)
        {
            OpenHatch(RHinge, true);
            OpenHatch(LHinge, false);
        }
        else
        {
            CloseHatch(RHinge, false);
            CloseHatch(LHinge, true);
        }

        //UpdateText();
    }

    private void OpenHatch(GameObject hinge, bool dir) //True: positive | False: negative 
    {
        Debug.Log("Hatch: " + hinge);
        int dirVal;

        if (dir)
        {
            dirVal = 1;
            if (hinge.transform.rotation.eulerAngles.z < (90 * dirVal))
            {
                float angle = (90 * Time.deltaTime) * dirVal;

                hinge.transform.Rotate(0, 0, angle);
            }
        }
        else
        {
            dirVal = -1;
            if (hinge.transform.rotation.eulerAngles.z > (90 * dirVal))
            {
                float angle = (90 * Time.deltaTime) * dirVal;

                hinge.transform.Rotate(0, 0, angle);
            }
        }

        /*
        if (hinge.transform.rotation.eulerAngles.z < (90 * dirVal))
        {
            float angle = (90 * Time.deltaTime) * dirVal;

            hinge.transform.Rotate(0, 0, angle);
        }
        */
    }

    private void CloseHatch(GameObject hinge, bool dir)
    {
        int dirVal;

        if (dir)
        {
            dirVal = 1;
            if (hinge.transform.rotation.eulerAngles.z < (0 * dirVal))
            {
                float angle = (90 * Time.deltaTime) * dirVal;

                hinge.transform.Rotate(0, 0, angle);
            }
        }
        else
        {
            dirVal = -1;
            if (hinge.transform.rotation.eulerAngles.z < (0 * dirVal))
            {
                float angle = (90 * Time.deltaTime) * dirVal;

                hinge.transform.Rotate(0, 0, angle);
            }
        }

        /*
        if (hinge.transform.rotation.eulerAngles.z < 0 * dirVal)
        {
            float angle = -(90 * Time.deltaTime) * dirVal;

            hinge.transform.Rotate(0, 0, angle);
        }
        */
    }

    private void UpdateText()
    {
        string[] cubeVel = new string[3];

        cubeVel[0] = cubeTest.GetComponent<Rigidbody>().velocity.x.ToString();
        cubeVel[1] = cubeTest.GetComponent<Rigidbody>().velocity.y.ToString();
        cubeVel[2] = cubeTest.GetComponent<Rigidbody>().velocity.z.ToString();

        debugText.text = "Velocity: ";
        debugText.text += ("\n      X: " + cubeVel[0]);
        debugText.text += ("\n      Y: " + cubeVel[1]);
        debugText.text += ("\n      Z: " + cubeVel[2]);
    }
}