using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private GameObject clockHandGO;
    [SerializeField] private SpriteRenderer clockHandSR;

    [SerializeField] private LayerMask layerMask;

    private Slashable nearestSlashable;

    [SerializeField] private float distScaleFactor;

    private float maxDist;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        transform.position = NearbySlashable.Instance.transform.position;
        nearestSlashable = NearbySlashable.Instance.GetClosestSlashable();


        if (nearestSlashable != null && PlayerController.Instance.isHoldingSword)
        {

            maxDist = nearestSlashable.GetComponent<CircleCollider2D>().radius;
            float currentDist = Vector3.Distance(transform.position, nearestSlashable.transform.position);

            //sets scale and opacity of the hand based on the distance to the enemy
            clockHandSR.transform.localScale = new Vector3(currentDist * distScaleFactor, currentDist * distScaleFactor, 1);
            clockHandSR.color = new Color(1, 1, 1, 1 - (currentDist / (maxDist)));

            float angle = Mathf.Atan2(nearestSlashable.transform.position.y - transform.position.y,
                          nearestSlashable.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            //gets angle to the nearest enemy

            // Set the rotation of the arrow GameObject
            clockHandGO.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            clockHandSR.color = new Color(1, 1, 1, 0);
        }
    }
}
