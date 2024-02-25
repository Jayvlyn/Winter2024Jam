using System;
using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [SerializeField, Range(0, 1)] private float slashDirPitchChangeAmount;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            instance = this;
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        
        source.pitch = 1.0f;
        source.PlayOneShot(clip);
    }

    public void PlayOneShotAtPitch(AudioClip clip, float pitch)
    {
        
        source.pitch = pitch;
        source.PlayOneShot(clip);
        source.pitch = 1.0f;
    }

    public void PlayOneShotOnSlashDir(AudioClip clip, float yDir)
    {
        
        source.pitch = 1.0f + (yDir * slashDirPitchChangeAmount);
        source.PlayOneShot(clip);
    }
    private void OnDestroy()
    {
        instance = null;
    }
}

