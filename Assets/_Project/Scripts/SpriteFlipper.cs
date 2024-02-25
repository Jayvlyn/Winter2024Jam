using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{

    private Vector2 lastPos;

    private void FixedUpdate()
    {
        float difference = ((Vector2)transform.position - lastPos).x;
        if (Mathf.Abs(difference) < 0.2) return;
        transform.localScale = new Vector3(transform.localScale.x * (Mathf.Sign(difference)), transform.localScale.y, 1);
        lastPos = transform.position;
    }
}
