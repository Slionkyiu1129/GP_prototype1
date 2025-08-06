using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoAlbumManager : MonoBehaviour
{
    [Header("UI 參考")]
    public List<Button> photoButtons; // Unity 中手動建立並拖曳的 Button
    public Image[] thumbnails;        // 對應 photoButtons 的圖片元件
    public GameObject[] lockIcons;    // 鎖圖示，依照解鎖狀態顯示
    public GameObject photoPreviewPanel;
    public Image previewImage;
    public TMP_Text previewDescription;
    public GameObject inventoryUI;    // 用來回到背包

    [Header("照片資料")]
    public List<PhotoData> allPhotos = new List<PhotoData>();

    private int currentIndex = 0;

    void Start()
    {
        photoPreviewPanel.SetActive(false);
        RefreshPhotoButtons();
    }

    void Update()
    {
        // 鍵盤選擇相簿內容
        if (photoButtons.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = Mathf.Max(currentIndex - 1, 0);
            HighlightButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = Mathf.Min(currentIndex + 1, photoButtons.Count - 1);
            HighlightButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OpenPhoto(allPhotos[currentIndex].id);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ClosePhotoAlbum();
        }
    }

    void RefreshPhotoButtons()
    {
        for (int i = 0; i < allPhotos.Count; i++)
        {
            if (i >= photoButtons.Count) break; // 避免資料多過 UI

            PhotoData data = allPhotos[i];

            // 設定縮圖與鎖圖
            if (thumbnails[i] != null)
                thumbnails[i].sprite = data.photo;

            if (lockIcons[i] != null)
                lockIcons[i].SetActive(!data.unlocked);

            photoButtons[i].interactable = data.unlocked;

            int id = data.id; // 避免 closure 問題
            photoButtons[i].onClick.RemoveAllListeners();
            photoButtons[i].onClick.AddListener(() => OpenPhoto(id));
        }

        currentIndex = 0;
        HighlightButton(currentIndex);
    }

    void HighlightButton(int index)
    {
        for (int i = 0; i < photoButtons.Count; i++)
        {
            Image bg = photoButtons[i].GetComponent<Image>();
            if (bg != null)
                bg.color = (i == index) ? Color.yellow : Color.white;
        }
    }

    public void OpenPhoto(int id)
    {
        PhotoData data = allPhotos.Find(p => p.id == id);
        if (data == null) return;

        previewImage.sprite = data.photo;
        previewDescription.text = data.description;
        photoPreviewPanel.SetActive(true);
    }

    public void ClosePreview()
    {
        photoPreviewPanel.SetActive(false);
    }

    public void ClosePhotoAlbum()
    {
        gameObject.SetActive(false); // 關閉相簿
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(true); // 返回背包
        }
    }

    public void UnlockPhoto(int id)
    {
        PhotoData data = allPhotos.Find(p => p.id == id);
        if (data != null && !data.unlocked)
        {
            data.unlocked = true;
            RefreshPhotoButtons();
        }
    }
}
