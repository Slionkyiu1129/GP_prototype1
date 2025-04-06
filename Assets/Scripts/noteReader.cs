using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteReader : MonoBehaviour
{
    public GameObject noteUI;
    private bool isNearNote = false;

    void Update()
    {
        if (isNearNote && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("pressed C");
            ShowNote();
        }
        else if (noteUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            HideNote();
        }
    }

    void ShowNote()
    {
        noteUI.SetActive(true);
    }

    void HideNote()
    {
        noteUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sprite"))
        {
            Debug.Log("OnTriggerEnter");
            isNearNote = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sprite"))
        {
            Debug.Log("OnTriggerExit");
            isNearNote = false;
        }
    }
}
