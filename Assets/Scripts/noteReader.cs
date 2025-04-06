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
        else if (noteUI.activeSelf && Input.GetKeyDown(KeyCode.C))
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sprite"))
        {
            Debug.Log("OnTriggerEnter");
            isNearNote = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sprite"))
        {
            Debug.Log("OnTriggerExit");
            isNearNote = false;
        }
    }
}
