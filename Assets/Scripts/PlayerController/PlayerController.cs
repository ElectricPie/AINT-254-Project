using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0.8f;

    private Rigidbody m_rigidbody;

    Vector3 temp;

    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        float xMouseInput = Input.GetAxis("Mouse X");

        temp = (transform.right* xInput +transform.forward * zInput) * m_speed;

        if (temp.x > 0.2)
            temp.x = 0.2f;

        if (temp.x < -0.2)
            temp.x = -0.2f;

        if (temp.z > 0.2)
            temp.z = 0.2f;

        if (temp.z < -0.2)
            temp.z = -0.2f;

        m_rigidbody.velocity += temp;

        transform.Rotate(0, xMouseInput, 0);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 150.0f, 100.0f, 30.0f), "Velocity");
        GUI.Label(new Rect(120.0f, 150.0f, 100.0f, 30.0f), temp.ToString());
    }
}

