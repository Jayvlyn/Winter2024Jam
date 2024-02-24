using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;
using EventHandler = Guymon.DesignPatterns.EventHandler;

public class TimeAffectable : MonoBehaviour
{
    [SerializeField] private float multiplier;
    private bool inTimeDilation;
    [SerializeField] private SpriteRenderer magicArea;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out TimeManipulator tm) )
        {
            tm.Add(this);
            if(tm.inAffect) StartTimeManipulation();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out TimeManipulator tm) )
        {
            tm.Remove(this);
            EndTimeManipulation();
        }
    }

    public void StartTimeManipulation()
    {
        inTimeDilation = true;
        magicArea.enabled = true;
    }
    public void EndTimeManipulation()
    {
        inTimeDilation = false;
        magicArea.enabled = false;
    }

    public float GetMultiplier()
    {
        
        return (inTimeDilation) ? multiplier : 1;
    }
}
