using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrect : MonoBehaviour
{
    public Transform correctSnapPoint;
    public float tolerance = 0.1f;

    public bool IsCorrectlyPlaced()
    {
        return Vector2.Distance(transform.position, correctSnapPoint.position) < tolerance;
    }
    public void ForceSnapToCorrect()
    {
        transform.position = correctSnapPoint.position;
    }
}
