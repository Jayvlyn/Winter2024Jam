using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    private string bestTime;
    private int bossDefeats;

    public TMP_Text bestTimeText;
    public TMP_Text bossDefeatsText;
    public GameObject redirectUI;

    private void OnEnable()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data.bestMinutes == int.MaxValue)
        {
            bestTimeText.text = "Best time: NONE";
        }
        else
        {
            bestTime = string.Format("{0:00}" + ":", data.bestMinutes);
            bestTime += string.Format("{0:00}" + ".", data.bestSeconds);
            bestTime += string.Format("{0:00}", data.bestMilliseconds);
            bestTimeText.text = "Best time: " + bestTime;
        }

        bossDefeats = data.bossDefeats;
        bossDefeatsText.text = "Boss defeats: " + bossDefeats.ToString();
    }

    public void OnBack()
    {
        if (redirectUI != null) redirectUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
