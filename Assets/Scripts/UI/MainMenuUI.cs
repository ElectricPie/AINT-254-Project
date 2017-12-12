using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
    //Private
    private GameObject m_loadWindow;

	// Use this for initialization
	void Start () {
        m_loadWindow = gameObject.transform.parent.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Continue()
    {
        //TODO
    }

    public void NewGame()
    {
        //TODO
    }

    public void LoadLevel()
    {
        m_loadWindow.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
