using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class was just for testing the save-load during implementation, disregard this file
public class DataTestPlayer : MonoBehaviour
{
    [SerializeField] public int level;
    [SerializeField] public float health;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
