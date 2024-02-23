using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private MovementType movement;
    
}

public enum MovementType
{
    Vertical,
    Horizontal,
    Circular
}