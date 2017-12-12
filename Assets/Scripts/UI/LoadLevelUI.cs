using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelUI : MonoBehaviour {
    //Public
    public int steps;
    public float TopHeight;
    public float BottomHeight;

    //Private
    private float m_totalHeight;
    private RectTransform m_panel;

    // Use this for initialization
    void Start () {
        m_panel = GetComponent<RectTransform>();
        m_totalHeight = m_panel.sizeDelta.y;

        CloseLoadWindow();
    }

    public void UpdateScroll(float value)
    {
        m_panel.anchoredPosition = new Vector2(0, (m_totalHeight * value) - m_totalHeight/2);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void CloseLoadWindow()
    {
        transform.parent.parent.gameObject.SetActive(false);
    }
}
