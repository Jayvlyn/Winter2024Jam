using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneQuickAdd : MonoBehaviour
{
    public bool doValidation = false;
    private void OnValidate()
    {
        if (doValidation)
            GetComponentInParent<RuneLayout>().AddRuneToList(gameObject);
    }
}
