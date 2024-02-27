using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneQuickAdd : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponentInParent<RuneLayout>().AddRuneToList(gameObject);
    }
}
