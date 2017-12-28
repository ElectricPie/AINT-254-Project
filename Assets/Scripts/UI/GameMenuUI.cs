using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
    private GameObject[] m_buttons;

    private bool m_pauseState;

    private PlayerController m_player;
    private GunController m_gun;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        m_pauseState = false;

        m_buttons = new GameObject[2];
        m_player = GameObject.Find("Player").GetComponent<PlayerController>();
        m_gun = GameObject.Find("Player").GetComponent<GunController>();

        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i] = transform.GetChild(i).gameObject;
            m_buttons[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause(m_pauseState);
        }
    }

    private void TogglePause(bool state)
    {
        if (state)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        
        

        m_pauseState = !m_pauseState;
        ToggleButtons(m_pauseState);
        m_player.Paused = m_pauseState;
        m_gun.Paused = m_pauseState;
    }

    private void ToggleButtons(bool state)
    {
        m_buttons[0].SetActive(state);
        m_buttons[1].SetActive(state);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitToDesktop()
    {
        Debug.Log("Quitting");
        Debug.Break();
        Application.Quit();
    }
}
