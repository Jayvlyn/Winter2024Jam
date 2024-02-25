using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashableDisabler : MonoBehaviour
{
    [SerializeField] private float timeToDisable;
    [SerializeField] private Slashable slashable;


    public void StartSlashDisable()
    {
        slashable.slashable = false;
        StartCoroutine(Delay(timeToDisable));
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        slashable.slashable = true;
    }
}
