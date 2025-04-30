using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Instance;

    private void Awake()
    {
        //Ensure that is only one InventoryManager
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //Ensure that InventoryManager wont be destroy or reload
        DontDestroyOnLoad(this);

        if (onInventoryCallBack == null)
        {
            onInventoryCallBack = delegate { };
        }

        Debug.Log("InventoryManager Initialized!");
    }
    #endregion

    public List<Item> ItemList = new List<Item>();
    public delegate void onInventoryChange();
    public onInventoryChange onInventoryCallBack;
    private DialogueManager dialogueManager;
    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    
    public void Add(Item newItem)
    {
        ItemList.Add(newItem);
        if (onInventoryCallBack != null)
        {
            onInventoryCallBack();
        }
        else
        {
            Debug.LogWarning("onInventoryCallBack is NULL! UI will not update.");
        }
    }

    public void Remove(Item oldItem)
    {
        ItemList.Remove(oldItem);
    }

    public void AddItem(string itemName, int amount)
    {
        // 檢查 ItemList 中是否已經有這個 item
        foreach (Item item in ItemList)
        {
            if (item.ItemName == itemName)
            {
                item.amount += amount;
                onInventoryCallBack?.Invoke();
                return;
            }
        }

        // 如果沒有，新增新的 item
        Item itemTemplate = Resources.Load<Item>("Items/" + itemName);

        if (itemTemplate == null)
        {
            Debug.LogError("Item " + itemName + " not found in Resources/Items!");
            return;
        }

        Item newItem = Instantiate(itemTemplate);
        newItem.amount = amount;
        ItemList.Add(newItem);

        onInventoryCallBack?.Invoke();
    }

    public void SyncFlyerFromDialogue()
    {
        if (dialogueManager != null)
        {
            int flyerNum = dialogueManager.GetFlyerNum();
            UpdateFlyerAmount(flyerNum);
        }
        onInventoryCallBack?.Invoke();
    }

    //public void UpdateFlyerAmount(int amount)
    //{
    //    // 把 ItemList 裡 flyer 的數量改掉
    //    foreach (Item item in ItemList)
    //    {
    //        if (item.ItemName == "flyer")
    //        {
    //            item.amount = amount;
    //            onInventoryCallBack?.Invoke();
    //            return;
    //        }
    //    }
    //}

    public void UpdateFlyerAmount(int newAmount)
    {
        Debug.Log("GOING ---> UpdateFlyerAmount(int newAmount)");
        foreach (Item item in ItemList)
        {
            if (item.ItemName == null)
            {
                Debug.Log("CANT FIND item.ItemName");
            }
            Debug.Log("ItemName = " + item.ItemName);
            if (item.ItemName == "flyer")
            {
                item.amount = newAmount;
                Debug.Log("Flyer Amount = "+ item.amount);
                onInventoryCallBack?.Invoke();
                return;
            }
        }

        // 如果沒有 flyer，新增一個
        //Item itemTemplate = Resources.Load<Item>("Items/flyer");
        //if (itemTemplate != null)
        //{
        //Debug.Log("itemTemplate != null");
        //Item newItem = Instantiate(itemTemplate);
        //newItem.amount = newAmount;
        //ItemList.Add(newItem);
        //onInventoryCallBack?.Invoke();
        // }
    }
}
