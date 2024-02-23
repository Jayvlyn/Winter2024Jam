using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private MovementType movement;
    [SerializeField] private Vector2 origin;

    private void Update()
    {
        switch (movement)
        {
            case MovementType.Circular:
            {
                
                break;
            }
            case MovementType.Horizontal:
            {
                break;
            }
            case MovementType.Vertical:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }
}

public enum MovementType
{
    Stationary,
    Vertical,
    Horizontal,
    Circular
}