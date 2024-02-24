using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;
using EventHandler = Guymon.DesignPatterns.EventHandler;

public class TimeManipulator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer castRender;
    private float initialAlpha;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventHandler.Invoke("StartCast", new EventArgsInt());
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            EventHandler.Invoke("EndCast", new EventArgsInt());
        }
    }
    
    

    
}

public class EventArgsInt : EventArgs
{
    public int t;
}