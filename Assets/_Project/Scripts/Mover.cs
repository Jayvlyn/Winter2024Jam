using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private MovementType movement;
    [SerializeField] private Vector2 radiusOrEnd;
    [SerializeField] private Vector2 centerOrStart;
    [SerializeField] private float speed;
    [SerializeField] public bool reverse;
    [SerializeField] private TimeAffectable timeEffect;


    private float elapsedTime;
    private bool direction;
    
    private void Update()
    {
        if (!reverse)
        {
            elapsedTime += GetTimeChange();
            if (elapsedTime > 360)
            {
                direction = !direction;
                elapsedTime = 0;
            }
        }
        else
        {
            elapsedTime -= GetTimeChange();
            if (elapsedTime < 0)
            {
                direction = !direction;
                elapsedTime = 360;
            }
        }
        switch (movement)
        {
            case MovementType.Circular:
            {
                float radius = Vector2.Distance(centerOrStart + (Vector2)origin.position, radiusOrEnd + (Vector2)origin.position);
                
                float x = Mathf.Cos(elapsedTime) * radius + centerOrStart.x + origin.position.x;
                float y = Mathf.Sin(elapsedTime) * radius + centerOrStart.y + origin.position.y;
                transform.position = new Vector2(x, y);
                break;
            }
            case MovementType.Straight:
            {
                transform.position = Vector2.Lerp(centerOrStart + (Vector2)origin.position, radiusOrEnd + (Vector2)origin.position, elapsedTime / 360);
                break;
            }
            case MovementType.PingPong:
            {
                transform.position = Vector2.Lerp(centerOrStart + (Vector2)origin.position, radiusOrEnd + (Vector2)origin.position,
                    (direction) ? (elapsedTime / 360) : 1 - (elapsedTime / 360));
                break;
            }
        }
    }

    private float GetTimeChange()
    {
        return Time.deltaTime * speed * (timeEffect == null ? 1 : timeEffect.GetMultiplier());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centerOrStart + (Vector2)origin.position, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(radiusOrEnd + (Vector2)origin.position, 0.5f);
    }
}

public enum MovementType
{
    Stationary,
    PingPong,
    Straight,
    Circular
}