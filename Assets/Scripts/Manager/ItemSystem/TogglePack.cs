using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePack : MonoBehaviour
{
    public GameObject inventoryUI;
    public player player;
    public static bool isInventoryOpen = false;

    void Start()
    {
        //Just ensure that UI is working correct
        inventoryUI.SetActive(true);  
        inventoryUI.SetActive(false);
        isInventoryOpen = false;
    }

    void Update()
    {
        // Open the pack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleInventory();
        }
    }


    private void ToggleInventory()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);
        isInventoryOpen = !isActive;
        // Ensure that Player cant move when pack is opening
        if (player != null)
        {
            player.enabled = isActive;
        }
    }
}
