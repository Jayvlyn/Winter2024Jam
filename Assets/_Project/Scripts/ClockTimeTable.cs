using System;
using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using Guymon.Graphics;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimeTable : Singleton<ClockTimeTable>
{
    [SerializeField] private Image clockImage;
    [SerializeField] private Image second;
    [SerializeField] private Image minute;
    [SerializeField] private Image hour;

    [SerializeField] public float bonusRotateSpeed;

    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private float extraClockFade;
    [SerializeField] public float totalTime;

    [SerializeField] private float fadeInSpeed;
    public float elapsedTime;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        clockImage.color = new Color(clockImage.color.r, clockImage.color.g, clockImage.color.b,
            elapsedTime * extraClockFade * fadeInSpeed / totalTime);
        
        second.color = new Color(second.color.r, second.color.g, second.color.b,
            elapsedTime * fadeInSpeed / totalTime);
        minute.color = new Color(minute.color.r, minute.color.g, minute.color.b,
            elapsedTime * fadeInSpeed / totalTime);
        hour.color = new Color(hour.color.r, hour.color.g, hour.color.b,
            elapsedTime * fadeInSpeed / totalTime);

        if (elapsedTime >= totalTime)
        {
            sceneChanger?.ChangeScene();
        }
    }
}
