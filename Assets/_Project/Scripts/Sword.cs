using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;
using UnityEngine.Serialization;

public class Sword : MonoBehaviour
{
    public IdleSword idle = new IdleSword();
    public StationarySword stationary = new StationarySword();
    public SlashingSword slashing = new SlashingSword();
    public ThrowingSword throwing = new ThrowingSword();
    
    
    public Rigidbody2D Rigidbody;
    public Collider2D ConnectedCollider;
    public float MoveForce;
    public float distanceTolerance = 0.1f;
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] public Transform FollowTransform;
    [HideInInspector] public float ElapsedThrowTime;
    [SerializeField] public float ThrowSpeedUpPercent;

    public void Solidity(bool solid)
    {
        ConnectedCollider.isTrigger = (!solid);
    }
    private void Start()
    {
        ChangeState(idle);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private SwordStateBase currentState;
    public void ChangeState(SwordStateBase state)
    {
        currentState?.OnExitState(this);
        currentState = state;
        currentState.OnEnterState(this);
    }

    public void MoveTowards(Transform target)
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= distanceTolerance)
        {
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        
        Rigidbody.AddForce(direction * MoveForce * (ElapsedThrowTime + 1));
    }
    

    [HideInInspector] public Stabable objectStabbedInto;
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

    [ContextMenu(nameof(Throw))]
    public void Throw()
    {
        ChangeState(throwing);
    }
    [ContextMenu(nameof(BecomeIdle))]
    public void BecomeIdle()
    {
        ChangeState(idle);
    }

    public void Slash()
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