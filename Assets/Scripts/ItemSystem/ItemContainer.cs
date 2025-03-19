using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item thisItem;
    private bool isPlayerNearby = false;

    private void Update()
    {
        //If pressed "T", then add add item to Inventory
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Pressed T");

            //Add item to inventory
            InventoryManager.Instance.Add(thisItem);
            InventoryManager.Instance.onInventoryCallBack();

            Destroy(gameObject);
        }
    }

    //Enter Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("PLayer entered trigger zone");
        }
    }

    //Exit Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left trigger zone");
        }
    }
}
