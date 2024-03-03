using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool speedrunTimer = false;
    public float volume;
    public float musicVolume;

    public SettingsData(SettingsMenu settings)
    {
        speedrunTimer = settings.speedRunTimerOn;
        volume = settings.volume;
        musicVolume = settings.musicVolume;
    }

    public SettingsData() {}
}
