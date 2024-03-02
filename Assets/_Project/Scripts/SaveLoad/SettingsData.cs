using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool speedrunTimer = false;

    public SettingsData(SettingsMenu settings)
    {
        speedrunTimer = settings.speedRunTimerOn;
    }

    public SettingsData() {}
}
