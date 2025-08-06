using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{
    public Transform[] spawnPoints; // 0號、1號、2號...

    public Transform GetSpawnPoint(int index)
    {
        if (index >= 0 && index < spawnPoints.Length)
            return spawnPoints[index];
        return spawnPoints[0]; // 預設
    }
}
