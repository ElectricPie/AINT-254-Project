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
    }
}
