using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool usesExternalTimer = true;
    private void Update()
    {
        if (usesExternalTimer)
        {
            transform.Rotate(Vector3.forward *
                (speed * Time.deltaTime * Mathf.Pow(ClockTimeTable.Instance.elapsedTime, ClockTimeTable.Instance.bonusRotateSpeed) / ClockTimeTable.Instance.totalTime), Space.Self);
            
        }
        else
        {
            transform.Rotate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
        }

    }
}
