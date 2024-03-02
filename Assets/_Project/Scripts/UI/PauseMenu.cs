using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    public GameObject settingsUI;
    public GameObject statsUI;

    public GameObject speedRunTimer;

    private void Start()
    {
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            if (speedRunTimer != null) speedRunTimer.SetActive(settingsData.speedrunTimer);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                if(settingsUI.activeSelf)
                {
                    //pauseUI.SetActive(true);
                    settingsUI.SetActive(false);
                }
                else if(statsUI.activeSelf)
                {
                    //pauseUI.SetActive(true);
                    statsUI.SetActive(false);
                }
                else if(pauseUI.activeSelf)
                {
                    Resume();
                }
            } else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }


    #region BUTTON EVENTS

    public void OnResume()
    {
        Resume();
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene("Game");
    }

    public void OnSettings()
    {
        settingsUI.SetActive(true);
    }

    public void OnStats()
    {
        statsUI.SetActive(true);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        // Save Data
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    #endregion
}
