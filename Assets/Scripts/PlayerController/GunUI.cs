using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour {
    public GunController gunCont;

    private Text[] m_values = new Text[3]; 

	// Use this for initialization
	void Start () {
        m_values[0] = gameObject.transform.GetChild(0).GetComponent<Text>();
        m_values[1] = gameObject.transform.GetChild(1).GetComponent<Text>();
        m_values[2] = gameObject.transform.GetChild(2).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        string polarity;

        
        if (gunCont.Polarity)
        {
            polarity = "Repel";
        }
        else
        {
            polarity = "Attract";
        }

        m_values[0].text = polarity;
        m_values[1].text = gunCont.Power.ToString();
        m_values[2].text = gunCont.WellCycle.ToString();
	}
}
