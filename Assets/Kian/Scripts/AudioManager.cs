using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
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
    }

    public void PlayOneShotOnSlashDir(AudioClip clip, float yDir)
    {
        source.pitch += yDir * .5f;
        source.PlayOneShot(clip);
    }
}
