using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guymon.DesignPatterns;
using UnityEngine.Serialization;

public class Sword : MonoBehaviour
{
    public ReelingSword reel = new ReelingSword();
    public StationarySword stationary = new StationarySword();
    public SlashingSword slashing = new SlashingSword();
    public ThrowingSword throwing = new ThrowingSword();
    public HoveringSword hovering = new HoveringSword();
    
    public Rigidbody2D Rigidbody;
    public Collider2D ConnectedCollider;
    public float IdleForce;
    public float ThrowForce;
    public float FollowForce;
    public float FollowForceMultiplier = 10;
    public float distanceTolerance = 0.1f;
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] public Transform FollowTransform;
    [HideInInspector] public float ElapsedThrowTime;
    [SerializeField] public float MovingSpeedUpPercent;

    public void Solidity(bool solid)
    {
        ConnectedCollider.isTrigger = (!solid);
    }
    private void Start()
    {
        ChangeState(reel);
    }

    private void Update()
    {
        ElapsedThrowTime += Time.deltaTime * MovingSpeedUpPercent;
    }

    private void FixedUpdate()
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

    public void MoveTowards(Transform target, float force, bool speedUp = true)
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= distanceTolerance)
        {
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        
        Rigidbody.AddForce(direction * force * ((speedUp) ? MovingSpeedUpPercent + 1 : 1));
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
}
