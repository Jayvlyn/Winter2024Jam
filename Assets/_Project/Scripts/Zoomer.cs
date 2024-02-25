using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cvm;

    [SerializeField] private float growSize;
    [SerializeField] private float time;
    private float smallSize;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            smallSize = cvm.m_Lens.OrthographicSize;
            StartCoroutine(SizeChange(smallSize, growSize, cvm, time));
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SizeChange(cvm.m_Lens.OrthographicSize, smallSize, cvm, time));
            
        }
    }

    IEnumerator SizeChange(float being, float end, CinemachineVirtualCamera view, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            view.m_Lens.OrthographicSize = Mathf.Lerp(being, end, elapsedTime / time);
            yield return null;
        }
    }
}
