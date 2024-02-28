using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] private Slider timeSlider;

    private float totalTime;
    [SerializeField] float maxTime;

    [SerializeField] float timePerSegment;

    [SerializeField] AudioClip thock;

    private float timeFreezeLength;

    private int currentSegment;
    public bool gameEnded = false;

    int minutes;
    int seconds;
    int milliseconds;
    string text;

    [SerializeField] private TMP_Text SpeedrunText;
    [SerializeField] private bool showSpeedrunTimer = false;
    private void Start()
    {
        totalTime = maxTime;
        currentSegment = 0;
    }


    private void Update()
    {
        if (timeFreezeLength >= 0)
        {
            timeFreezeLength -= Time.deltaTime;
        }

        if (totalTime <= 0 && !gameEnded)
        {
            SceneChanger.instance.ChangeScene();
            gameEnded = true;
        }

        if (!gameEnded)
        {
            milliseconds += (int)(Time.deltaTime * 1000);
            if (milliseconds > 1000)
            {
                seconds++;
                milliseconds = 0;
            }

            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }
        }

        if (timeFreezeLength <= 0)
        {
            totalTime -= Time.deltaTime;

        }

        text = string.Format("{0:00}" + ":", minutes);
        text += string.Format("{0:00}" + ".", seconds);
        text += string.Format("{0:00}", milliseconds);
        SpeedrunText.text = text;
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (totalTime < maxTime - (timePerSegment * (currentSegment + 1)) && !gameEnded)
        {
            currentSegment++;
            AudioManager.instance.PlayOneShot(thock);
        }

        timeSlider.value = 1 - (timePerSegment / maxTime) * currentSegment;
    }

    public void FreezeTime(float time)
    {
        timeFreezeLength = time;
    }

    private void GameOver()
    {
        SceneChanger.instance.ChangeScene();
    }
}
