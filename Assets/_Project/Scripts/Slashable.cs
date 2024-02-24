using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Slashable : MonoBehaviour
{
    private NearbySlashable connectedSlasher;
    [SerializeField] int health;
    [SerializeField] private UnityEvent slashThroughEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out NearbySlashable playerSlasher))
        {
            connectedSlasher = playerSlasher;
            playerSlasher.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out NearbySlashable playerSlasher))
        {
            connectedSlasher = null;
            playerSlasher.Remove(this);
        }
    }

    public void OnSlashThrough(int damage)
    {
        health -= damage;
        slashThroughEvent?.Invoke();

        if (health <= 0) Death();
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
