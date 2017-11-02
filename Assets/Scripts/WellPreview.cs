using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellPreview : MonoBehaviour {
    public Material repelMat;
    public Material attractMat;

    private GunController gunCont;
    
	// Use this for initialization
	void Start () {
        gunCont = GameObject.Find("Player").GetComponent<GunController>();
	}
	 
	// Update is called once per frame
	void Update () {
        Material temp;

        if (gunCont.Polarity)
        {
            temp = repelMat;
        }
        else
        {
            temp = attractMat;
        }

        transform.GetChild(0).GetComponent<Renderer>().material = temp;

    }
}
