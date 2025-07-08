using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // �ϥ� TextMeshPro

public class InventoryUIManager : MonoBehaviour
{
    public GameObject itemSlotPrefab; // �Ω���ܪ� prefab
    public Transform itemSlotParent;  // UI Panel �� Content �ϰ�
    public Image itemImageUI;
    public TMP_Text itemInfoText;
    private int currentIndex = 0; // For choosing the things in inventory
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;

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

    void Update()
    {
        // Only when UI is opening
        if (gameObject.activeSelf && currentSlots.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndex = Mathf.Max(currentIndex - 1, 0);
                HighlightSlot(currentIndex);
                ShowItemInfo(currentIndex);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
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

        // ���M���ª� slot
        foreach (var slot in currentSlots)
        {
            Destroy(slot);
        }
        currentSlots.Clear();

        // ��ܨC�Ӫ��~
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
        TMP_Text nameText = currentSlots[i].transform.Find("ItemNameText").GetComponent<TMP_Text>();
        TMP_Text amountText = currentSlots[i].transform.Find("ItemAmountText").GetComponent<TMP_Text>();

        Color targetColor = (i == index) ? highlightColor : normalColor;
        nameText.color = targetColor;
        amountText.color = targetColor;
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
