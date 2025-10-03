using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkFishCorrect : MonoBehaviour
{
    public CapsuleCollider2D correctTriggerArea;
    private bool isInCorrectArea = false;

    private void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.gameObject == correctTriggerArea.gameObject)
        {
            isInCorrectArea = true;
            Debug.Log("Fish is in the correct area.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == correctTriggerArea.gameObject)
        {
            isInCorrectArea = false;
        }
    }

    public bool IsCorrectlyPlaced()
    {
        return isInCorrectArea;
    }

    public void ForceSnapToCorrect()
    {
        //transform.position = correctTriggerArea.transform.position;
        // GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;  //這個方法不可行
        GetComponent<fishPushed>().canBePush = false; //使漁網不可以被推動
    }
}
