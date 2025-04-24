using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrect : MonoBehaviour
{
    public Transform correctSnapPoint; // ���w���T���l����m
    public float tolerance = 0.1f; // �e�t�d�򤺺⥿�T

    public bool IsCorrectlyPlaced()
    {
        return Vector2.Distance(transform.position, correctSnapPoint.position) < tolerance;
    }
    public void ForceSnapToCorrect()
    {
        transform.position = correctSnapPoint.position;
    }
}
