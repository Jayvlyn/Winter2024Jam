using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private Sword sword;
    [SerializeField] private Transform swordPoint;
    [SerializeField] private float maxThrowDistance;
    [SerializeField] private LayerMask hitLayer;

    public void Throw(Vector2 direction, Vector2 origin)
    {
        Debug.DrawRay(origin, direction * maxThrowDistance);
        RaycastHit2D raycast = Physics2D.Raycast(origin, direction, maxThrowDistance, hitLayer);
        swordPoint.position = raycast.point;
        
        sword.ChangeState(sword.throwing);
        realingIn = false;
    }

    private bool realingIn;
    public void RealIn()
    {
        if (realingIn) return;
        sword.ChangeState(sword.idle);
    }

    public float Catch(Transform playerPos)
    {
        return Vector2.Distance(playerPos.position, sword.transform.position);
        realingIn = false;
    }
}
