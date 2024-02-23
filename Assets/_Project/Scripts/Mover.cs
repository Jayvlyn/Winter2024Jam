using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private MovementType movement;
    [SerializeField] private Vector2 radiusOrEnd;
    [SerializeField] private float speed;
    [SerializeField] private bool reverse;

    private void Start()
    {
        origin = transform.position;
    }

    private Vector2 origin;
    private float elapsedTime;
    private bool direction;
    
    private void Update()
    {
        if (!reverse)
        {
            elapsedTime += Time.deltaTime * speed;
            if (elapsedTime >= 360)
            {
                direction = !direction;
                elapsedTime = 0;
            }
        }
        else
        {
            elapsedTime -= Time.deltaTime * speed;
            if (elapsedTime <= 0)
            {
                direction = !direction;
                elapsedTime = 360;
            }
        }
        switch (movement)
        {
            case MovementType.Circular:
            {
                float radius = Vector2.Distance(origin, radiusOrEnd);
                
                float x = Mathf.Cos(elapsedTime) * radius + origin.x;
                float y = Mathf.Sin(elapsedTime) * radius + origin.y;
                transform.position = new Vector2(x, y);
                break;
            }
            case MovementType.Straight:
            {
                transform.position = Vector2.Lerp(origin, radiusOrEnd, elapsedTime / 360);
                break;
            }
            case MovementType.PingPong:
            {
                transform.position = Vector2.Lerp(origin, radiusOrEnd,
                    (direction) ? (elapsedTime / 360) : 1 - (elapsedTime / 360));
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(origin, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(radiusOrEnd, 0.5f);
    }
}

public enum MovementType
{
    Stationary,
    PingPong,
    Straight,
    Circular
}