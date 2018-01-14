using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    private PlayerProgression m_progression;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        m_progression = new SaveLoad().Load();

        if (m_progression == null)
        {
            m_progression = new PlayerProgression();
            new SaveLoad().Save(m_progression);
        }

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void ChangeLastLevel(int value)
    {
        m_progression.LastLevel = value;
    }

    public int MaxLevel
    {
        set { Progression.MaxLevel = value; }
        get { return m_progression.MaxLevel; }
    }


    public PlayerProgression Progression
    {
        get { return m_progression; }
    }
}
