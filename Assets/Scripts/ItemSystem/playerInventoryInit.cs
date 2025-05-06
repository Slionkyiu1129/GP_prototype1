using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventoryInit : MonoBehaviour
{
    void Start()
    {
        Item camera = Resources.Load<Item>("Items/camera");
        Item photoAlbum = Resources.Load<Item>("Items/photoAlbum");

        if (camera != null) InventoryManager.Instance.AddItem("Camera", 1);
        if (photoAlbum != null) InventoryManager.Instance.AddItem("PhotoAlbum", 1);
    }
}
