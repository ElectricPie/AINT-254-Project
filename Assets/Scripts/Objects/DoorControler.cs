using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour {
    private Animator m_anim;

    private bool m_open;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public void Open()
    {
        m_open = true;
        m_anim.SetBool("Open", true);
    }

    public void Close()
    {
        m_open = false;
        m_anim.SetBool("Open", false);
    }
}
