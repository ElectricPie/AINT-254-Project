using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGun : MonoBehaviour {
    public GameObject gun;


	// Use this for initialization
	void Start () {
        gun.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        gun.SetActive(true);
    }
}
