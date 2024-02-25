using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{

    [SerializeField, Range(0, 1)] private float slashDirPitchChangeAmount;

    private AudioSource GetSource()
    {
        return GetComponent<AudioSource>();
    }


    public void PlayOneShot(AudioClip clip)
    {
        AudioSource source = GetSource();
        source.pitch = 1.0f;
        source.PlayOneShot(clip);
    }

    public void PlayOneShotAtPitch(AudioClip clip, float pitch)
    {
        AudioSource source = GetSource();
        source.pitch = pitch;
        source.PlayOneShot(clip);
        source.pitch = 1.0f;
    }

    public void PlayOneShotOnSlashDir(AudioClip clip, float yDir)
    {
        AudioSource source = GetSource();
        source.pitch = 1.0f + (yDir * slashDirPitchChangeAmount);
        source.PlayOneShot(clip);
    }
}
