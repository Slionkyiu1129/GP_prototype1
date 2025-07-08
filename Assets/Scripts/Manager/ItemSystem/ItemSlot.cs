using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    Item item;
    public Image ItemImage;
    public TextMeshProUGUI ItemName;
    public Image HighlightImage;

    public void AddItem(Item thisItem)
    {
        item = thisItem;

        ItemImage.sprite = item.ItemImage;
        ItemName.text = item.ItemName;

        ItemImage.enabled = true;
        SetHighlight(false);
    }

    public void RemoveItem()
    {
        //Remove Item from List
        InventoryManager.Instance.Remove(item);
        InventoryManager.Instance.onInventoryCallBack();
    }

    public void Clean()
    {
        item = null;

        ItemImage.sprite = null;
        ItemName.text = "Empty";

        ItemImage.enabled = false;
        SetHighlight(false);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.UseItem();
            RemoveItem();
        }
        else
        {
            return;
        }
    }

    // Select Hover
    public void SetHighlight(bool state)
    {
        HighlightImage.gameObject.SetActive(state); 
    }
}
