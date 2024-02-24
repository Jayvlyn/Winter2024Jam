using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;
using EventHandler = Guymon.DesignPatterns.EventHandler;

public class TimeManipulator : MonoBehaviour
{
    [SerializeField] public float range = 6;
    [SerializeField] private SpriteRenderer castRender;
    private float initialAlpha;
    private List<TimeAffectable> tms = new List<TimeAffectable>();
    public bool inAffect;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            castRender.enabled = true;
            inAffect = true;
            foreach (var ta in tms)
            {
                ta.StartTimeManipulation();
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            castRender.enabled = false;
            inAffect = false;
            foreach (var ta in tms)
            {
                ta.EndTimeManipulation();
            }
        }
    }

    public void Add(TimeAffectable a)
    {
        tms.Add(a);
    }
    public void Remove(TimeAffectable a)
    {
        tms.Remove(a);
    }
    

    
}
