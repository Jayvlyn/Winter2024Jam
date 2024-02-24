using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GemManager : MonoBehaviour
{
    private bool gem1Alive = true;
    [SerializeField] private Light2D gem1Light;

    private bool gem2Alive = true;
    [SerializeField] private Light2D gem2Light;

    private bool gem3Alive = true;
    [SerializeField] private Light2D gem3Light;

    public void Gem1Destroyed()
    {
        gem1Alive = false;
        gem1Light.enabled = false;
        CheckGemsDestroyed();
    }

    public void Gem2Destroyed()
    {
        gem2Alive = false;
        gem2Light.enabled = false;
        CheckGemsDestroyed();
    }

    public void Gem3Destroyed()
    {
        gem3Alive = false;
        gem3Light.enabled = false;
        CheckGemsDestroyed();
    }

    private void CheckGemsDestroyed()
    {
        if (!gem1Alive && !gem2Alive && !gem3Alive)
        {
            GetComponent<Slashable>().slashable = true;
            GetComponent<Slashable>().SetKillable(true);

        }
    }
}
