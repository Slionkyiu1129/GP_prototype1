using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // 使用 TextMeshPro

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemSlotPrefab; // 用於顯示的 prefab
    public Transform itemSlotParent;  // UI Panel 的 Content 區域

    private List<GameObject> currentSlots = new List<GameObject>();

    private void OnEnable()
    {
        // 訂閱 inventory 更新事件
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryCallBack += UpdateUI;
        }
        UpdateUI();
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryCallBack -= UpdateUI;
        }
    }

    void UpdateUI()
    {
        Debug.Log("Updating inventory UI");

        if (itemSlotPrefab == null)
        {
            Debug.LogError("ItemSlotPrefab is NULL!");
        }
        if (itemSlotParent == null)
        {
            Debug.LogError("ItemSlotParent is NULL!");
        }

        foreach (var item in InventoryManager.Instance.ItemList)
        {
            Debug.Log("Item in inventory: " + item.ItemName + " x" + item.amount);
        }

        // 先清掉舊的 slot
        foreach (var slot in currentSlots)
        {
            Destroy(slot);
        }
        currentSlots.Clear();

        // 顯示每個物品
        foreach (var item in InventoryManager.Instance.ItemList)
        {
            GameObject newSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            TMP_Text nameText = newSlot.transform.Find("ItemNameText").GetComponent<TMP_Text>();
            TMP_Text amountText = newSlot.transform.Find("ItemAmountText").GetComponent<TMP_Text>();

            nameText.text = item.ItemName;
            amountText.text = "x" + item.amount.ToString();

            currentSlots.Add(newSlot);
        }
    }
}
