using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
    public int LevelIndex;

    private Game m_progression;

    // Use this for initialization
    void Start () {
        m_progression = GameObject.Find("Progression").GetComponent<Game>();
        m_progression.MaxLevel = LevelIndex;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
