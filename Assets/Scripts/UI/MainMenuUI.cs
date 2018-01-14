using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {
    //Private
    private GameObject m_loadWindow;
    private Game m_progression;

    // Use this for initialization
    void Start () {
        m_loadWindow = gameObject.transform.parent.GetChild(1).gameObject;
        m_progression = GameObject.Find("Progression").GetComponent<Game>();
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
        new SaveLoad().Save(m_progression.Progression);
        Application.Quit();
    }
}
