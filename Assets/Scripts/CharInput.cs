using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInput : MonoBehaviour {
    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 10.0f, 100.0f, 30.0f), "Horizontal Axis:");
        GUI.Label(new Rect(120.0f, 10.0f, 100.0f, 30.0f), Input.GetAxis("Horizontal").ToString());

        GUI.Label(new Rect(5.0f, 25.0f, 100.0f, 30.0f), "Vertical Axis:");
        GUI.Label(new Rect(120.0f, 25.0f, 100.0f, 30.0f), Input.GetAxis("Vertical").ToString());

        
        GUI.Label(new Rect(5.0f, 45, 100.0f, 30.0f), "Cam Horizontal:");
        GUI.Label(new Rect(120.0f, 45.0f, 100.0f, 30.0f), Input.GetAxis("Mouse X").ToString());

        GUI.Label(new Rect(5.0f, 60.0f, 100.0f, 30.0f), "Cam Vertical:");
        GUI.Label(new Rect(120.0f, 60.0f, 100.0f, 30.0f), Input.GetAxis("Mouse Y").ToString());
        

        GUI.Label(new Rect(5.0f, 100.0f, 100.0f, 30.0f), "Jump:");
        GUI.Label(new Rect(120.0f, 100.0f, 100.0f, 30.0f), Input.GetAxis("Jump").ToString());
    }
}
    