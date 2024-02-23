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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
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

    public void OnSettings()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(true);
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