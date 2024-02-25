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

    public bool playerFacingRight;

    public Rigidbody2D Rigidbody;
    public Collider2D ConnectedCollider;
    public float IdleForce;
    public float ThrowForce;
    public float FollowForce;
    public float FollowForceMultiplier = 10;
    public float distanceTolerance = 0.1f;
    public float flingForce = 10;
    [SerializeField] public Transform PlayerTransform;
    [SerializeField] public Transform FollowTransform;
    [HideInInspector] public float ElapsedThrowTime;
    [SerializeField] public float MovingSpeedUpPercent;
    [SerializeField] public float stabOffset = 1.4f;

    [SerializeField] public AudioClip wallStabSound;

    public void Solidity(bool solid)
    {
        ConnectedCollider.isTrigger = (!solid);
        if (solid) gameObject.layer = (LayerMask.NameToLayer("Ground"));
        else gameObject.layer = (LayerMask.NameToLayer("Sword"));
    }
    private void Start()
    {
        ChangeState(hovering);
    }

    private void Update()
    {
        ElapsedThrowTime += Time.deltaTime * MovingSpeedUpPercent;
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    public SwordStateBase currentState;
    public void ChangeState(SwordStateBase state)
    {
        if (state == currentState) return;
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

        if ((Rigidbody.velocity.x < 0 && !isFacingRight) || (Rigidbody.velocity.x > 0 && isFacingRight)) Flip();
    }

    public bool isFacingRight;
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent(out PlayerController pc) && currentState == stationary && pc.gameObject.transform.position.y < transform.position.y && pc.rb.velocity.y > 0)
            {
                pc.rb.velocity = Vector2.up * flingForce;
                pc.animator.SetTrigger("Pull");
                
                //Vector2 velocity = playerRb.velocity;
                //playerRb.AddForce(Vector3.up * flingForce, ForceMode2D.Impulse);
            }
        }
    }
}
