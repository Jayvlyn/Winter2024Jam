using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRuneSpawner : Singleton<BossRuneSpawner>
{
    [SerializeField] List<GameObject> runeLayouts = new List<GameObject>();

    bool bossActive = false;

    public bool canSpawnLayout;

    private void Update()
    {
        if (!bossActive) return;


        if (canSpawnLayout)
        {
            //TODO: play animation or audio

            Instantiate(runeLayouts[Random.Range(0, runeLayouts.Count)], transform.position, transform.rotation, transform);
            canSpawnLayout = false;
        }
    }

    public void SetBossActive()
    {
        bossActive = true;
        canSpawnLayout = true;
    }
}
