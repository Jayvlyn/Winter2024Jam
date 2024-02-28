using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject statsUI;

    #region BUTTON EVENTS
    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSettings()
    {
        settingsUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnStats()
    {
        statsUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    #endregion
}
