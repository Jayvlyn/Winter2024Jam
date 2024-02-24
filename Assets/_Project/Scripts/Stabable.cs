using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Stabable : MonoBehaviour
{
    private Sword attachedSword;
    [SerializeField] private UnityEvent OnEnterStab;
    [SerializeField] private UnityEvent OnExitStab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Sword sword))
        {
            attachedSword = sword;
            sword.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Sword sword))
        {
            sword.Remove(this);
            attachedSword = null;
        }
    }

    public void BeginStab()
    {
        OnEnterStab?.Invoke();
    }
    public void EndStab()
    {
        OnExitStab?.Invoke();
    }
}
