using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // able to save in file
public class PlayerData
{





    #region TESTING
    public int level;
    public float health;
    public float[] position; // 3 elements, one for each position

    public PlayerData(DataTestPlayer player)
    {
        level = player.level;
        health = player.health;

        // Cannot serialize unity-specific data, so must convert to an array of floats for Vector3
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    #endregion
}
