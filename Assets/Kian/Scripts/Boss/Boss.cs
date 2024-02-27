using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] BossRuneSpawner normalSpawns;
    [SerializeField] RuneSpawner specialSpawns;

    public int chandelierHits = 0;
    public bool active = false;

    private GameObject recentLayout;

    private bool canSpawnLayout;
    public bool settingRunes = false;

    [SerializeField] private bool startActive = false;

    private void Update()
    {
        
    }

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
            GameObject lastRune = recentLayout.GetComponent<RuneLayout>().GetLastItem();
            if (lastRune != null)
            {
                lastRune.GetComponent<Rune>().lastRuneInLayout = false;
                recentLayout.GetComponent<RuneLayout>().PlaySound();
            }
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
        active = true;
        StartCoroutine(CallSpawns("set Boss active"));
    }


    public IEnumerator CallSpawns(string whereFrom)
    {
        if (!active) yield return null;
        Debug.Log("Called callSpawns from " + whereFrom);
        settingRunes = true;
        yield return new WaitForSeconds(3);
        if (!active) yield return null;
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
        RuneSpawner.active = true;
    }
}
