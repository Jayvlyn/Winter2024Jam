using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public int stepCount = 0;

    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;
    public AudioClip step4;

    public void OnStep()
    {
        stepCount++;
        if(stepCount % 2 == 0) // EVEN STEP
        {
            if(Random.Range(1,3) == 1) AudioManager.instance.PlayOneShot(step1);
            else                       AudioManager.instance.PlayOneShot(step3);
        }
        else // ODD STEP
        {
            if (Random.Range(1, 3) == 1) AudioManager.instance.PlayOneShot(step2);
            else                         AudioManager.instance.PlayOneShot(step4);
        }
    }
}
