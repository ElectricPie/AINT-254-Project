using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        float yInput = -Input.GetAxis("Mouse Y");
        transform.Rotate(yInput, 0, 0);
        

        /*
        float yInput = -Input.GetAxis("Mouse Y");
        
        Debug.Log("Rotation: " + transform.localRotation.x);

        if (transform.localRotation.x > -0.6 && transform.localRotation.x + yInput < 0.6)
        {
            transform.Rotate(yInput, 0, 0);
        }
        else if (transform.localRotation.x >= -0.6 && yInput > 0)
        {
             
        }
        else if (transform.localRotation.x >= 0.6 && yInput < 0)
        {

        }
        */
    }
}
