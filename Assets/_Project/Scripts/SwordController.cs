using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] public Sword sword;
    [SerializeField] private Transform swordPoint;
    [SerializeField] private float maxThrowDistance;
    [SerializeField] private LayerMask hitLayer;

    [SerializeField] private float forwardSlashDistance;

    [SerializeField, Range(0, 1)] private float lerpFollow;



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

    public void Slash(Transform playerPos, Vector2 direction, float time)
    {
        StartCoroutine(RotateTowards(playerPos, direction, time));

    }

    IEnumerator RotateTowards(Transform origin, Vector2 direction, float time)
    {
        float elapsedTime = 0;
        float finalAngle = Mathf.Atan2(direction.y,
            direction.x) * Mathf.Rad2Deg + 90f;
        
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            sword.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(sword.transform.rotation.z, finalAngle, lerpFollow));
            sword.transform.position = Vector3.Lerp(sword.transform.position, ((Vector2)origin.position + (direction * forwardSlashDistance)), lerpFollow);
            yield return null;
        }
    }
}
