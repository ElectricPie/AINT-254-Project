using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Other: " + other.tag);
    }

    private void OnParticleTrigger()
    {
        Debug.Log("Partical");
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Other Part: " + other.tag);
    }
}
