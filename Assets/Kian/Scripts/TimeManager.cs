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

    private int currentSegment;

    private void Start()
    {
        totalTime = maxTime;
        currentSegment = 0;
    }


    private void Update()
    {
        totalTime -= Time.deltaTime;
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (totalTime < maxTime - (timePerSegment * (currentSegment + 1)))
        {
            currentSegment++;
        }

        timeSlider.value = 1 - (timePerSegment / maxTime) * currentSegment;
    }
}