using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellIndicatorUI : MonoBehaviour {
    //Public
    public Sprite[] indicatorSprites = new Sprite[3];
    public Sprite[] indicatorBorderSprites = new Sprite[2];

    //Private
    private Image[] m_indicators;
    private Image[] m_borderIndicator;

    private GravityWell[] m_gravityWells;

	// Use this for initialization
	void Start () {
        m_indicators = new Image[3];
        m_borderIndicator = new Image[3];
        m_gravityWells = new GravityWell[3];

        GameObject gravityWells = GameObject.Find("GravtiyWells");

        //Gets the indicator UI gameobjects
        for (int i = 0; i < m_indicators.Length; i++)
        {
            m_indicators[i] = transform.GetChild(i).gameObject.GetComponent<Image>();
            m_borderIndicator[i] = transform.GetChild(i + 3).gameObject.GetComponent<Image>();

            m_gravityWells[i] = gravityWells.transform.GetChild(i).GetComponent<GravityWell>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < m_gravityWells.Length; i++)
        {
            if (m_gravityWells[i].Polarity)
            {
                m_indicators[i].sprite = indicatorSprites[1];
            }
            else
            {
                m_indicators[i].sprite = indicatorSprites[2];
            }
        }
	}

    public void UpdateLook(int index)
    {
        for (int i = 0; i < m_borderIndicator.Length; i++)
        {
            m_borderIndicator[i].sprite = indicatorBorderSprites[0];
        }

        if (index >= 0)
        {
            m_borderIndicator[index].sprite = indicatorBorderSprites[1];
        }
    }
}
