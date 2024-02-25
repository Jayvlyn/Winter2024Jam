using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public GameObject redirectUI;
    public AudioMixer mixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] _resolutions;

    private void Start()
    {
        int currentResIndex = 0;
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> stringRes = new List<string>();
        for(int i = 0; i < _resolutions.Length; i++)
        {
            stringRes.Add($"{_resolutions[i].width} X {_resolutions[i].height}");
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(stringRes);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetRes(int index)
    {
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
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

    public void SetVolume(float value)
    {
        mixer.SetFloat("Volume", value);
    }

    public void SetFullScreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    
}
