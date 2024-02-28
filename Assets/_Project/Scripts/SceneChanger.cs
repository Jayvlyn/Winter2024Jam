using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;


    //[SerializeField] private Rigidbody2D rb;
    [SerializeField] private string sceneName;

    [SerializeField] private Boss boss;

    [SerializeField] private RuneLayout runeCircle;

    [SerializeField] private Transform topOfHead;
    [SerializeField] private Transform arenaFloor;

    [SerializeField] private GameObject chandelier;

    [SerializeField] private GameObject rune;

    [SerializeField] private AudioClip bossLaugh;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }
    private void Start()
    {
    }

    public void PickScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ChangeScene()
    {
        StartCoroutine(ShortFinish());
    }

    public void ChangeSceneSoon(float time)
    {
        PlayerController.Instance.hasControl = false;

        if (boss.chandelierHits == 3 && boss != null)
        {
            StartCoroutine(StopChandelier());
            TimeManager.Instance.gameEnded = true;
        }
        //StartCoroutine(QuietAudio());

        if (boss.active)
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
            boss.active = false;
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
        yield return new WaitUntil(() => Vector3.Distance(chandelier.transform.position, topOfHead.position) < 2);

        chandelier.GetComponent<Rigidbody2D>().gravityScale = 0;
        chandelier.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        chandelier.transform.position = topOfHead.transform.position;
        Instantiate(rune, chandelier.transform.position, transform.rotation);

    }

    private IEnumerator WaitForPlayerLand()
    {

        yield return new WaitUntil(() => Mathf.Abs(PlayerController.Instance.transform.position.y - arenaFloor.position.y) < 3);
        PlayerController.Instance.hasControl = false;

        PlayerController.Instance.animator.SetFloat("MoveSpeed", 0);
        PlayerController.Instance.animator.SetBool("Skid", false);
        StartCoroutine(FinishScene());


    }

    private IEnumerator FinishScene()
    {
        PlayerController.Instance.hasControl = false;
        AudioManager.instance.PlayOneShot(bossLaugh);
        yield return new WaitForSeconds(4.5f);
        Instantiate(runeCircle, arenaFloor.transform.position, transform.rotation);
        yield return new WaitForSeconds(8.5f);
        SceneManager.LoadScene(sceneName);

    }

    private IEnumerator ShortFinish()
    {
        AudioManager.instance.PlayOneShot(bossLaugh);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(sceneName);

    }
    //private IEnumerator QuietAudio()
    //{
    //    Zoomer.bossMusic.volume -= 0.05f;
    //    Zoomer.originalMusic.volume -= 0.05f;
    //    yield return new WaitUntil(() => Zoomer.bossMusic.volume <= 0 && Zoomer.originalMusic.volume <= 0);
    //}
}
