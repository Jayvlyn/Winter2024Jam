using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuneQuickAdd))]
public class Rune : MonoBehaviour
{
    [SerializeField] float runeLifespan;
    [SerializeField] bool lastRuneInLayout = false;


    public void Activate()
    {
        Destroy(gameObject, runeLifespan);
    }

    private void OnDestroy()
    {
        if (lastRuneInLayout)
        {
            GetComponentInParent<RuneLayout>().GetComponentInParent<Boss>().OnFailed();
            GetComponentInParent<RuneLayout>().PlaySound();
        }
    }
}
