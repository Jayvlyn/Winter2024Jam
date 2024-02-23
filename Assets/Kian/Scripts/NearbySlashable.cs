using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbySlashable : Singleton<NearbySlashable>
{
    [SerializeField] List<GameObject> closeObjects = new List<GameObject>();

    public void Add(Slashable s)
    {

    }

    public void Remove(Slashable s)
    {
        
    }
}
