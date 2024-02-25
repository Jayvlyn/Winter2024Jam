using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbySlashable : MonoBehaviour
{
    [SerializeField] List<Slashable> closeObjects = new List<Slashable>();

    [SerializeField] private LayerMask blockLayer;

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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, s.transform.position - transform.position, distance, blockLayer);
            //Debug.DrawRay(transform.position, (s.transform.position - transform.position).normalized * distance);
            //if(hit.collider != null) Debug.Log(hit.collider.gameObject);
            if (distance < closestDistance && s.slashable && hit.collider == null)
            {
                closestDistance = distance;
                closestSlashable = s;
            }
        }

        return closestSlashable;
    }
}
