using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneLayout : MonoBehaviour
{
    [SerializeField] List<GameObject> runes = new List<GameObject>();

    [SerializeField] float baseTimeBetweenRunes;
    [SerializeField, Range(0f, 1f)] float decrementPercentage;

    private void Start()
    {
        StartCoroutine(SpawnRunes());
    }

    public void SectionEnd()
    {
        BossRuneSpawner.Instance.canSpawnLayout = true;
        Destroy(gameObject);
    }

    private IEnumerator SpawnRunes()
    {
        for (int i = 0; i < runes.Count; i++) 
        {
            float waitTime = baseTimeBetweenRunes * (1 / (decrementPercentage * i + 1));
            print(waitTime);
            yield return new WaitForSeconds(waitTime);
            runes[i].SetActive(true);
        }

    }
}
