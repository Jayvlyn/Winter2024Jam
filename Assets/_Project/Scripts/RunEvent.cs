using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;

public class RunEvent : MonoBehaviour
{
    [SerializeField] private string eventName;
    public void Call()
    {
        EventHandler.Invoke(eventName, new EventArgsInt());
    }
}

public class EventArgsInt : EventArgs
{
    
}
