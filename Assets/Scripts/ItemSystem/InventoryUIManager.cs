using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // 使用 TextMeshPro

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemSlotPrefab; // 用於顯示的 prefab
    public Transform itemSlotParent;  // UI Panel 的 Content 區域
    public Image itemImageUI;
    public TMP_Text itemInfoText;
    private int currentIndex = 0; // For choosing the things in inventory
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

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

    void Update()
    {
        // Only when UI is opening
        if (gameObject.activeSelf && currentSlots.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentIndex = Mathf.Max(currentIndex - 1, 0);
                HighlightSlot(currentIndex);
                ShowItemInfo(currentIndex);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentIndex = Mathf.Min(currentIndex + 1, currentSlots.Count - 1);
                HighlightSlot(currentIndex);
                ShowItemInfo(currentIndex);
            }
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
        for (int i = 0; i < InventoryManager.Instance.ItemList.Count; i++)
        {
            var item = InventoryManager.Instance.ItemList[i];
            GameObject newSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            TMP_Text nameText = newSlot.transform.Find("ItemNameText").GetComponent<TMP_Text>();
            TMP_Text amountText = newSlot.transform.Find("ItemAmountText").GetComponent<TMP_Text>();

            nameText.text = item.ItemName;
            amountText.text = "x" + item.amount.ToString();

            currentSlots.Add(newSlot);
        }
        // reset index & highlight
        currentIndex = 0;
        HighlightSlot(currentIndex);
        ShowItemInfo(currentIndex);
    }

    void HighlightSlot(int index)
    {
        for (int i = 0; i < currentSlots.Count; i++)
        {
            Image slotImage = currentSlots[i].GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.color = (i == index) ? highlightColor : normalColor;
            }
        }
    }

    void ShowItemInfo(int index)
    {
        if (index >= 0 && index < InventoryManager.Instance.ItemList.Count)
        {
            Item selectedItem = InventoryManager.Instance.ItemList[index];
            itemImageUI.sprite = selectedItem.ItemImage;
            itemInfoText.text = selectedItem.description;
        }
        else
        {
            itemImageUI.sprite = null;
            itemInfoText.text = "";
        }
    }
}
