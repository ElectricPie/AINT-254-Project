using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour {

    public GunController gunCont;


    private int m_dmgTime = 1;

    private bool m_dmg;

    private float m_dmgCounter;

    private GameObject m_player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Damage the player
        if (m_dmg && m_dmgCounter < 0)
        {
            m_player.GetComponent<PlayerController>().Health = -1;
            m_dmgCounter = m_dmgTime;
        }
        else if (m_dmg)
        {
            m_dmgCounter -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_player = other.gameObject;
            m_dmg = true;
            m_dmgCounter = m_dmgTime;
            gunCont.Disable = true;
        }

        
        if (other.tag == "GravityWell")
        {
            Invoke("gunCont.ClearWell(other.gameObject)", 1f);
            gunCont.ClearWell(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_dmg = false;
            gunCont.Disable = false;
        }
    }
}
