using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbySlashable : Singleton<NearbySlashable>
{
    [SerializeField] List<Slashable> closeObjects = new List<Slashable>();

    public void Add(Slashable s)
    {
        closeObjects.Add(s);
    }

    public void Remove(Slashable s)
    {
        closeObjects.Remove(s);
    }

    public Slashable GetClosestSlashable()
    {
        float closestDistance = 100.0f;
        Slashable closestSlashable = null;

        foreach(Slashable s in closeObjects)
        {
            float distance = Vector3.Distance(transform.position, s.transform.position);
            if (distance < closestDistance && s.slashable)
            {
                closestDistance = distance;
                closestSlashable = s;
            }
        }

        return closestSlashable;
    }
}
