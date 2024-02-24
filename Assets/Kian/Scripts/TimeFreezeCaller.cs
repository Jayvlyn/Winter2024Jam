using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeCaller : MonoBehaviour
{
    public void FreezeTime(float time)
    {
        TimeManager.Instance.FreezeTime(time);
    }
}
