using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reverser : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0, 100)] private Vector2 interval;
    [SerializeField] private Mover mover;

    private float elapsedTime;
    private void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0)
        {
            elapsedTime = Random.Range(interval.x, interval.y);
            mover.reverse = !mover.reverse;
        }
    }
}
