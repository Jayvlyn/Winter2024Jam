using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Zoomer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cvm;

    [SerializeField] private float growSize;
    [SerializeField] private float time;
    private float smallSize;

    public AudioSource originalMusic;
    public AudioSource bossMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            smallSize = cvm.m_Lens.OrthographicSize;
            StartCoroutine(SizeChange(smallSize, growSize, cvm, time, Vector2.zero, offset));
            originalMusic.volume = 0;
            bossMusic.volume = .6f;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SizeChange(cvm.m_Lens.OrthographicSize, smallSize, cvm, time, offset, Vector2.zero));
            originalMusic.volume = .6f;
            bossMusic.volume = 0;
        }
    }

    IEnumerator SizeChange(float being, float end, CinemachineVirtualCamera view, float time, Vector2 init, Vector2 destination)
    {
        float elapsedTime = 0;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            view.m_Lens.OrthographicSize = Mathf.Lerp(being, end, elapsedTime / time);
            highPos.localPosition = Vector2.Lerp(init, destination, elapsedTime / time);
            yield return null;
        }
    }

    [SerializeField] private Transform highPos;
    [SerializeField] private Vector2 offset;
}
