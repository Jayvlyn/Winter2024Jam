using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
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
    private bool gameEnded = false;
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

        if (timeFreezeLength <= 0)
            totalTime -= Time.deltaTime;
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
