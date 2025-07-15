using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform spawnUp;
    public Transform spawnDown;
    public Transform spawnLeft;
    public Transform spawnRight;

    void Start()
    {
        string entryDir = PathManager.Instance.entryPointDirection;
        Transform spawnPos = null;

        switch (entryDir)
        {
            case "Up":
                spawnPos = spawnUp;
                break;
            case "Down":
                spawnPos = spawnDown;
                break;
            case "Left":
                spawnPos = spawnLeft;
                break;
            case "Right":
                spawnPos = spawnRight;
                break;
            default:
                spawnPos = spawnDown;
                break; // 預設從下方進入
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && spawnPos != null)
        {
            player.transform.position = spawnPos.position;
        }
    }
}
