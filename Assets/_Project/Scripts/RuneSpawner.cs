using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
using EventArgs = Guymon.DesignPatterns.EventArgs;
using EventHandler = Guymon.DesignPatterns.EventHandler;
using Random = UnityEngine.Random;

public class RuneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject runePrefab;
    [SerializeField] private List<Transform> parents = new List<Transform>();
    [SerializeField, MinMaxSlider(0, 100)] private Vector2 spawnTimer;
    [SerializeField, MinMaxSlider(0, 100)] private Vector2 spawnHeight;
    [SerializeField] private int maxSpawnCount;

    private int spawnCount;

    private float timer;


    private void OnEnable()
    {
        EventHandler.AddListener("RuneDied", decrementSpawnCount);
    }
    private void OnDisable()
    {
        EventHandler.RemoveListener("RuneDied", decrementSpawnCount);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(spawnTimer.x, spawnTimer.y);
            if(spawnCount < maxSpawnCount) Spawn();
        }
    }

    private void Spawn()
    {
        spawnCount++;
        int getter = Random.Range(0, 2);
        Instantiate(runePrefab, new Vector3(0, spawnHeight[getter], 0) + transform.position,
            quaternion.identity, parents[getter]);
    }

    private void decrementSpawnCount(EventArgs args)
    {
        spawnCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnHeight.x);
        Gizmos.DrawWireSphere(transform.position, spawnHeight.y);
    }



}
