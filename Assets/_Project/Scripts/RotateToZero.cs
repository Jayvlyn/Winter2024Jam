using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToZero : MonoBehaviour
{

    [SerializeField] private float targetAngle;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float force;
    
    private void Update()
    {
        float currentAngle = transform.rotation.z;
        if (currentAngle > 180) currentAngle -= 360;
        if (currentAngle < -180) currentAngle += 360;
        Debug.Log(currentAngle);
        rigidbody.AddTorque((currentAngle - targetAngle) * force);
    }
}
