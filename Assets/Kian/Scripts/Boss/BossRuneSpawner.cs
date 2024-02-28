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


    public void SpawnEasyLayout(string whereFrom)
    {
        //Debug.Log("called easy layout from " + whereFrom);
        recentLayout = Instantiate(easyRuneLayouts[Random.Range(0, easyRuneLayouts.Count)], spawnPos.position, transform.rotation, transform);
        GetComponent<Boss>().SetRecentLayout(recentLayout);
    }

    public void SpawnHardLayout(string whereFrom)
    {
        //Debug.Log("called hard layout from " + whereFrom);
        recentLayout = Instantiate(hardRuneLayouts[Random.Range(0, hardRuneLayouts.Count)], spawnPos.position, transform.rotation, transform);
        GetComponent<Boss>().SetRecentLayout(recentLayout);
    }
}
