using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;

public class Sword : StateMessenger2D<Sword.SwordState>
{
    private IdleSword idle = new IdleSword(SwordState.Idle);
    private StationarySword slicing = new StationarySword(SwordState.Slicing);
    private StationarySword stationary = new StationarySword(SwordState.Stationary);
    private ThrowingSword throwing = new ThrowingSword(SwordState.Throwing);
    
    public Rigidbody2D Rigidbody;
    public float MoveForce;
    [HideInInspector] public Transform Target;

    public enum SwordState
    {
        Idle,
        Slicing,
        Throwing,
        Stationary
    }

    private void Awake()
    {
        ChangeState(SwordState.Idle);
    }

    public void MoveTowards()
    {
        Vector2 direction = Target.position - transform.position;
        Rigidbody.AddForce(direction * MoveForce);
    }

    private Stabable objectStabbedInto;
    public void Add(Stabable stabable)
    {
        objectStabbedInto = stabable;
    }
    public void Remove(Stabable stabable)
    {
        if (objectStabbedInto == stabable)
        {
            objectStabbedInto = null;
        }
    }

    public void Throw()
    {
        
    }
}



/*
 * 
    private void Update()
    {
        if (objectStabbedInto != null)
        {
            //Stuck in Wall
            //Don't move
            return;
        }
        float distanceFromPlayer = Vector2.Distance(transform.position, playerDestination.position);
        if (currentTarget == playerDestination)
        {
            if (distanceFromPlayer <= 0.1f)
            {
                //Too Close Don't Move
                return;
            }
        }

        Vector2 direction = currentTarget.position - transform.position;
        rb.AddForce(direction * (distanceFromPlayer + 1) * acc);
    }

*/