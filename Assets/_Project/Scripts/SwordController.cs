using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] public Sword sword;
    [SerializeField] private Transform swordPoint;
    [SerializeField] private float maxThrowDistance;
    [SerializeField] private LayerMask hitLayer;



    public void Throw(Vector2 direction, Vector2 origin, bool playerFacingRight)
    {
        Debug.DrawRay(origin, direction * maxThrowDistance);
        RaycastHit2D raycast = Physics2D.Raycast(origin, direction, maxThrowDistance, hitLayer);
        if (raycast.collider == null) raycast.point = origin + (direction * maxThrowDistance);
        swordPoint.position = raycast.point;
        
        sword.ChangeState(sword.throwing);
        reelingIn = false;

        sword.playerFacingRight = playerFacingRight;
    }

    public bool reelingIn;
    public void ReelIn()
    {
        if (reelingIn) return;
        
        sword.ChangeState(sword.reel);
    }

    public float Catch(Transform playerPos)
    {
        return Vector2.Distance(playerPos.position, sword.transform.position);
    }
}
