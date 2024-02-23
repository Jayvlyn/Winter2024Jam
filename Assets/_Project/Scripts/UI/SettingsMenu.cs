using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingsMenu : MonoBehaviour
{
    public GameObject redirectUI;

    public void OnBack()
    {
        if (redirectUI != null) redirectUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnSave()
    {
        // Save settings
        // SaveSystem.SaveSettings()
    }
}
