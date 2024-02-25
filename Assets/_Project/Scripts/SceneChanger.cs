using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneSoon(float time)
    {
        StartCoroutine(ChangeSoon(time));
    }

    IEnumerator ChangeSoon(float time)
    {
        yield return new WaitForSeconds(time);
        ChangeScene();

    }
}
