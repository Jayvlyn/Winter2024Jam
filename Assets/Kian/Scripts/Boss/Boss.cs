using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] BossRuneSpawner normalSpawns;
    [SerializeField] RuneSpawner specialSpawns;

    private int chandelierHits = 0;

    private bool canSpawnLayout;

    [SerializeField] private bool startActive = false;
    private void Start()
    {
        if (startActive)
        {
            SetBossActive();
        }
    }

    public void OnPlayerHitChandelier()
    {
        chandelierHits++;

        canSpawnLayout = true;

        if (chandelierHits == 2)
        {
            StartCoroutine(SetSpecialSpawns());
        }
        else
        {
            specialSpawns.active = false;
        }
    }

    public void OnFailed()
    {
        StartCoroutine(CallSpawns());
    }

    public void SetBossActive()
    {
        StartCoroutine(CallSpawns());
    }


    public IEnumerator CallSpawns()
    {
        Debug.Log("called CallSpawns coroutine");
        if (chandelierHits == 2)
            StartCoroutine(SetSpecialSpawns());
        yield return new WaitForSeconds(3);
        if (chandelierHits == 0)
        {
            Debug.Log("Should call Easy Spawn again");
            normalSpawns.SpawnEasyLayout();
        }
        else if (chandelierHits == 1)
        {
            Debug.Log("Should call hard spawn again");
            normalSpawns.SpawnHardLayout();
        }

    }


    private IEnumerator SetSpecialSpawns()
    {
        yield return new WaitForSeconds(3);
        specialSpawns.active = true;
    }
}
