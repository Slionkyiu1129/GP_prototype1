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
        // 先從 Resources 找到對應的 Item 資源
        Item itemTemplate = Resources.Load<Item>("Items/" + itemName);

        if (itemTemplate == null)
        {
            Debug.LogError("Item " + itemName + " not found in Resources/Items!");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            // 複製一份出來
            Item newItem = Instantiate(itemTemplate);
            ItemList.Add(newItem);
        }

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

    public void UpdateFlyerAmount(int amount)
    {
        // 把 ItemList 裡 flyer 的數量改掉
        foreach (Item item in ItemList)
        {
            if (item.ItemName == "flyer")
            {
                item.amount = amount;
                onInventoryCallBack?.Invoke();
                return;
            }
        }
    }
}
