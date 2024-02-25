using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRuneSpawner : Singleton<BossRuneSpawner>
{
    [SerializeField] List<GameObject> runeLayouts = new List<GameObject>();

    [SerializeField] private Transform spawnPos;

    [SerializeField] private AudioClip spawnSound;



    public GameObject recentLayout;


    public void SpawnLayout()
    {
        AudioManager.instance.PlayOneShotAtPitch(spawnSound, Random.Range(.9f, 1.2f));
        recentLayout = Instantiate(runeLayouts[Random.Range(0, runeLayouts.Count)], spawnPos.position, transform.rotation, transform);

    }
    
}
