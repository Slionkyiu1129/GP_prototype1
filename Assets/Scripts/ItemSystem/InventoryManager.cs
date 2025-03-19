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
            onInventoryCallBack = delegate { }; // 確保不會是 NULL
        }

        Debug.Log("InventoryManager Initialized!");
    }
    #endregion

    public List<Item> ItemList = new List<Item>();
    public delegate void onInventoryChange();
    public onInventoryChange onInventoryCallBack;

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
}
