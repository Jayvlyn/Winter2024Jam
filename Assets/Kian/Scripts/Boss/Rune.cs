using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuneQuickAdd))]
public class Rune : MonoBehaviour
{
    [SerializeField] float runeLifespan;
    public bool lastRuneInLayout = false;


    public void Activate()
    {
        Destroy(gameObject, runeLifespan);
    }

    private void OnDestroy()
    {
        if (lastRuneInLayout)
        {
            var boss = GetComponentInParent<Boss>();
            if(boss != null) boss.OnFailed();
            //GetComponentInParent<RuneLayout>().PlaySound();
        }
    }
}
