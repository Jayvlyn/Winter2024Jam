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

        if (chandelierHits < 2)
        {
            StartCoroutine(CallSpawns());
        }

        if (chandelierHits == 2)
        {
            specialSpawns.active = true;
        }
        chandelierHits++;
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
