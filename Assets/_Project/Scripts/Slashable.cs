
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Slashable : MonoBehaviour
{
    private NearbySlashable connectedSlasher;

    [SerializeField] private GameObject slashEffect;
    [SerializeField] private AudioClip hitSound;
    //[SerializeField] private AudioClip deathSound;


    public bool slashable = true;
    [SerializeField] private bool canDie;
    [SerializeField] GameObject ObjectToDestroy;

    [SerializeField] int health;
    [SerializeField] private UnityEvent slashThroughEvent;
    [SerializeField] private UnityEvent deathEvent;
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

        if(slashEffect != null) Instantiate(slashEffect, transform.position, transform.rotation);
        if(hitSound != null) AudioManager.Instance.PlayOneShotAtPitch(hitSound, Random.Range(0.6f, 1.4f));

        if (health <= 0 && canDie) Death();
    }

    public void Death()
    {
        //AudioManager.Instance.PlayOneShotAtPitch(deathSound, Random.Range(0.8f, 1.2f));
        Destroy(ObjectToDestroy);
        deathEvent?.Invoke();
    }

    public void SetKillable(bool killable)
    {
        canDie = killable;
    }
}
