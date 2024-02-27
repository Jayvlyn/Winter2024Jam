using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] BossRuneSpawner normalSpawns;
    [SerializeField] RuneSpawner specialSpawns;

    private int chandelierHits = 0;

    private GameObject recentLayout;

    private bool canSpawnLayout;
    public bool settingRunes = false;

    [SerializeField] private bool startActive = false;
    private void Start()
    {
        if (startActive)
        {
            SetBossActive();
        }
    }

    public void SetRecentLayout(GameObject layout)
    {
        recentLayout = layout;
    }

    public void OnPlayerHitChandelier()
    {
        chandelierHits++;

        if (recentLayout != null)
        {
            recentLayout.GetComponent<RuneLayout>().PlaySound();
            GameObject lastRune = recentLayout.GetComponent<RuneLayout>().GetLastItem();
            if (lastRune != null) lastRune.GetComponent<Rune>().lastRuneInLayout = false;
            Destroy(recentLayout);
        }

        canSpawnLayout = true;

        if (!settingRunes)
        {
            StartCoroutine(CallSpawns("OnPlayerHitChandelier"));
        }
    }

    public void OnFailed()
    {
        StartCoroutine(CallSpawns("OnFailed"));
    }

    public void SetBossActive()
    {
        StartCoroutine(CallSpawns("set Boss active"));
    }


    public IEnumerator CallSpawns(string whereFrom)
    {
        Debug.Log("Called callSpawns from " + whereFrom);
        settingRunes = true;
        yield return new WaitForSeconds(3);
        if (chandelierHits == 0)
        {
            normalSpawns.SpawnEasyLayout("call spawns coroutine");
        }
        else if (chandelierHits == 1)
        {
            normalSpawns.SpawnHardLayout("call spawns coroutine");
        }
        else
            StartCoroutine(SetSpecialSpawns());

    }


    private IEnumerator SetSpecialSpawns()
    {
        Debug.Log("Should have started special spawns");
        yield return new WaitForSeconds(3);
        specialSpawns.active = true;
    }
}
