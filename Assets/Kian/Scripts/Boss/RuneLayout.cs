using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneLayout : MonoBehaviour
{
    [SerializeField] List<GameObject> runes = new List<GameObject>();

    [SerializeField] float baseTimeBetweenRunes;
    [SerializeField, Range(0f, 1f)] float decrementPercentage;
    [SerializeField] private AudioClip spawnSound;


    private void Start()
    {
        StartCoroutine(SpawnRunes());
    }

    private IEnumerator SpawnRunes()
    {
        for (int i = 0; i < runes.Count; i++) 
        {
            float waitTime = baseTimeBetweenRunes * (1 / (decrementPercentage * i + 1));
            print(waitTime);
            yield return new WaitForSeconds(waitTime);
            runes[i].SetActive(true);
            runes[i].GetComponent<Rune>().Activate();
            PlaySound();
        }

    }

    //this is for RuneQuickAdd script
    public void AddRuneToList(GameObject rune)
    {
        if (!runes.Contains(rune))
            runes.Add(rune);
    }

    public void PlaySound()
    {
        AudioManager.instance.PlayOneShotAtPitch(spawnSound, Random.Range(.9f, 1.2f));
    }
}
