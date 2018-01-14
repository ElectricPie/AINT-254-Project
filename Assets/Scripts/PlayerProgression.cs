using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerProgression {
    public static int m_maxLevelIndex;
    private static int m_lastLevel;

    public PlayerProgression()
    {
        m_maxLevelIndex = 1;
        m_lastLevel = 1;
    }

    public int LastLevel {
        set { m_lastLevel = value; }
        get { return m_lastLevel; }
    }

    public int MaxLevel
    {
        set {
            if (value > m_maxLevelIndex)
            {
                m_maxLevelIndex = value;
            }
        }
        get { return m_maxLevelIndex; }
    }
}
