using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;

public class RuneDestroyedEvent : MonoBehaviour
{
    
    public void RuneDeath()
    {
        EventHandler.Invoke("RuneDied", new EventArgsInt());
    }
}

public class EventArgsInt : EventArgs
{
    
}
