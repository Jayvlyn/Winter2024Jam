using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject redirectUI;
    public AudioMixer mixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMP_Text timerToggleText;
    public Toggle timerToggle;
    public Toggle fullscreenToggle;
    private Resolution[] _resolutions;
    public GameObject speedRunTimer;

    private bool settingsJustOpened = false;

    // Settings data
    public bool speedRunTimerOn = false;

    private void OnEnable()
    {
        StartCoroutine(openTimer());

        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            timerToggle.isOn = settingsData.speedrunTimer;
        }

        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            int bossDefeats = data.bossDefeats;
            if(bossDefeats > 0)
            {
                timerToggle.interactable = true;
                timerToggleText.color = new Color(timerToggleText.color.r, timerToggleText.color.g, timerToggleText.color.b, 1);
            }
            else
            {
                timerToggle.interactable = false;
                timerToggleText.color = new Color(timerToggleText.color.r, timerToggleText.color.g, timerToggleText.color.b, .2f);
            }
        }

        fullscreenToggle.isOn = Screen.fullScreen;

        int currentResIndex = 0;
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> stringRes = new List<string>();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            stringRes.Add($"{_resolutions[i].width} X {_resolutions[i].height} {_resolutions[i].refreshRateRatio}hz");
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(stringRes);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        Debug.Log(settingsJustOpened);
    }

    public void SetRes(int index)
    {
        if(!settingsJustOpened)
        {
            Resolution resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
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

    public void SetSpeedrunTimer(bool toggle)
    {
        if(speedRunTimer != null)
        {
            speedRunTimer.SetActive(toggle);
        }
        speedRunTimerOn = toggle;
        SaveSystem.SaveSettings(this);
    }

    private IEnumerator openTimer()
    {
        settingsJustOpened = true;
        yield return new WaitForSecondsRealtime(0.2f);
        settingsJustOpened = false;
    }
}
