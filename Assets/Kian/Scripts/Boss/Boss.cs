using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] BossRuneSpawner normalSpawns;
    [SerializeField] RuneSpawner specialSpawns;

    private int chandelierHits = 0;

    public void OnPlayerHitChandelier()
    {
        chandelierHits++;

        if (chandelierHits < 2)
        {
            StartCoroutine(CallSpawns());
        }

        else if (chandelierHits == 2)
        {
            specialSpawns.active = true;
        }
        else
        {
            specialSpawns.active = false;
        }
    }

    public void OnFailed()
    {
        if (chandelierHits < 2)
        {
            StartCoroutine(CallSpawns());
        }
    }

    public void SetBossActive()
    {
        normalSpawns.SpawnLayout();
    }


    private IEnumerator CallSpawns()
    {
        yield return new WaitForSeconds(3);
        normalSpawns.SpawnLayout();
    }

}
