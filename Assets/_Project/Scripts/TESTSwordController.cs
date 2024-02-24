using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSwordController : MonoBehaviour
{
    [SerializeField] private Sword sword;
    [SerializeField] private Transform swordThrowFollowObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //sword.Throw(swordThrowFollowObject);
        }
    }
}
