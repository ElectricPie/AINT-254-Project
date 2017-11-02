using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjButton : Button
{
    public GameObject target;

    //private Button btnController;

    private bool active;

	// Use this for initialization
	void Start () {
        //btnController = GameObject.Find("BtnController").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Tag: " + other.gameObject.tag);

        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Player")
        {
            transform.position += new Vector3(0, -0.05f, 0);
                
            active = true;

            TriggerOn(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            transform.localPosition = new Vector3(0, 0.3f, 0);
        }

        TriggerOff(target);

        active = false;
    }

}
