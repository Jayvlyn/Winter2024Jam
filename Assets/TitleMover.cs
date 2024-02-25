using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMover : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    private bool start = false;

    private void Start()
    {
        StartCoroutine(MoveTitle());
    }

    void Update()
    {
        if(start && transform.position != targetPoint.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint.position, Time.deltaTime * 10);
        }
    }

    private IEnumerator MoveTitle()
    {
        yield return new WaitForSeconds(1);
        start = true;
    }
}
