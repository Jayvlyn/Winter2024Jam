using Guymon.DesignPatterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRuneSpawner : Singleton<BossRuneSpawner>
{
    [SerializeField] List<GameObject> easyRuneLayouts = new List<GameObject>();
    [SerializeField] List<GameObject> hardRuneLayouts = new List<GameObject>();

    [SerializeField] private Transform spawnPos;




    public GameObject recentLayout;


    public void SpawnEasyLayout()
    {
        recentLayout = Instantiate(easyRuneLayouts[Random.Range(0, easyRuneLayouts.Count)], spawnPos.position, transform.rotation, transform);
    }

    public void SpawnHardLayout()
    {
        recentLayout = Instantiate(hardRuneLayouts[Random.Range(0, hardRuneLayouts.Count)], spawnPos.position, transform.rotation, transform);
    }
}
