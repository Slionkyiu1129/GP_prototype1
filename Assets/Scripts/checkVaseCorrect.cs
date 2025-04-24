using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrect : MonoBehaviour
{
    public Transform correctSnapPoint; // 指定正確的吸附位置
    public float tolerance = 0.1f; // 容差範圍內算正確

    public bool IsCorrectlyPlaced()
    {
        return Vector2.Distance(transform.position, correctSnapPoint.position) < tolerance;
    }
    public void ForceSnapToCorrect()
    {
        transform.position = correctSnapPoint.position;
    }
}
