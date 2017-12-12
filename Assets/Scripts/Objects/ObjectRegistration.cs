using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRegistration : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Debug.Log("Adding \"" + gameObject + "\" to the list");
        GameObject.Find("ObjectController").GetComponent<ObjectController>().NewObject = gameObject; //Adds the object to the list
	}   
}
