using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Slashable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Slasher playerSlasher))
        {
            playerSlasher.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Slasher playerSlasher))
        {
            playerSlasher.Remove(this);
        }
    }
}
