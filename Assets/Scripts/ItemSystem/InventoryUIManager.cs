using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // �ϥ� TextMeshPro

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemSlotPrefab; // �Ω���ܪ� prefab
    public Transform itemSlotParent;  // UI Panel �� Content �ϰ�

    private List<GameObject> currentSlots = new List<GameObject>();

    private void OnEnable()
    {
        // �q�\ inventory ��s�ƥ�
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

        // ���M���ª� slot
        foreach (var slot in currentSlots)
        {
            Destroy(slot);
        }
        currentSlots.Clear();

        // ��ܨC�Ӫ��~
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
