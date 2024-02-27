using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private string sceneName;

    [SerializeField] private RuneLayout runeCircle;

    [SerializeField] private Transform topOfHead;
    [SerializeField] private Transform arenaFloor;

    [SerializeField] private GameObject chandelier;

    [SerializeField] private GameObject rune;

    [SerializeField] private AudioClip bossLaugh;
    
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneSoon(float time)
    {
        PlayerController.Instance.hasControl = false;

        if (Boss.chandelierHits == 3)
            StartCoroutine(StopChandelier());
        StartCoroutine(QuietAudio());

        if (Boss.active)
        {
            StartCoroutine(WaitForPlayerLand());
            if (RuneSpawner.active)
            {
                RuneSpawner.active = false;
                foreach (GameObject rune in RuneSpawner.spawnList)
                {
                    Destroy(rune);
                }
            }
            Boss.active = false;
        }
        else
            StartCoroutine(FinishScene());
        //StartCoroutine(ChangeSoon(time));
    }

    IEnumerator ChangeSoon(float time)
    {
        yield return new WaitForSeconds(time);
        ChangeScene();

    }

    private IEnumerator StopChandelier()
    {
        Debug.Log("Called StopChandelier Coroutine");
        yield return new WaitUntil(() => Vector3.Distance(chandelier.transform.position, topOfHead.position) < 5);

        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;

        Instantiate(rune, chandelier.transform.position, transform.rotation);

    }

    private IEnumerator WaitForPlayerLand()
    {

        yield return new WaitUntil(() => Mathf.Abs(PlayerController.Instance.transform.position.y - arenaFloor.position.y) < 3);

        StartCoroutine(FinishScene());


    }

    private IEnumerator FinishScene()
    {
        AudioManager.instance.PlayOneShot(bossLaugh);
        yield return new WaitForSeconds(4.5f);
        Instantiate(runeCircle, arenaFloor.transform.position, transform.rotation);
        yield return new WaitForSeconds(10);
        ChangeScene();
    }

    private IEnumerator QuietAudio()
    {
        Zoomer.bossMusic.volume -= 0.05f;
        Zoomer.originalMusic.volume -= 0.05f;
        yield return new WaitUntil(() => Zoomer.bossMusic.volume <= 0 && Zoomer.originalMusic.volume <= 0);
    }
}
