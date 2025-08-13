using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class PhotoData
{
    public int id; // 照片 ID（唯一值）
    public Sprite photo; // 縮圖/大圖（用同一張即可）

    [TextArea]
    public string description; // 文字描述
    public bool unlocked; // 是否已解鎖（僅做「初始狀態」參考，真實狀態以存檔為準）
}

[Serializable]
public class PhotoSaveData
{
    public List<int> unlockedPhotoIDs = new List<int>();
}

public class PhotoAlbumManager : MonoBehaviour
{
    [Header("UI 參考（請在 Inspector 指派）")]
    [Tooltip("相簿格子的按鈕清單（例如 9 個），順序會決定顯示排列順序。")]
    public List<Button> photoButtons; // 預先放好的按鈕格子（不使用 Prefab）
    public GameObject photoPreviewPanel; // 大圖/說明的面板
    public Image previewImage; // 大圖顯示
    public TMP_Text previewDescription; // 文字描述
    public GameObject inventoryUI; // 背包 UI（按 X 回去用）
    public bool IsAlbumActive =>
        gameObject.activeSelf || (photoPreviewPanel != null && photoPreviewPanel.activeSelf);

    [Header("照片資料（預設 9 張）")]
    public List<PhotoData> allPhotos = new List<PhotoData>();

    [Header("選取外觀")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.65f, 0.8f, 1f);

    // 內部狀態
    private List<Button> _visibleButtons = new List<Button>(); // 目前有顯示（已解鎖且有格子）的按鈕
    private int _currentIndex = -1;
    private string _savePath;
    
    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "photoAlbum.json");
    }

    private void Start()
    {
        // 初始化 UI
        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(false);

        // 先讀檔（若無檔則以 Inspector 的 unlocked 初始狀態建立存檔）
        LoadPhotoAlbum();

        // 套用到 UI
        RefreshPhotoButtons();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        // 沒有任何可見按鈕時，不處理移動/開啟
        bool hasAny = _visibleButtons.Count > 0;

        // 上下選擇
        if (hasAny && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            _currentIndex = Mathf.Max(_currentIndex - 1, 0);
            UpdateHighlight();
        }
        else if (hasAny && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            _currentIndex = Mathf.Min(_currentIndex + 1, _visibleButtons.Count - 1);
            UpdateHighlight();
        }
        // 開啟預覽
        else if (
            hasAny && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        )
        {
            var data = GetPhotoDataByVisibleIndex(_currentIndex);
            if (data != null)
                OpenPhoto(data.id);
        }

        // X：若預覽面板開著，先關預覽；否則關相簿並回到背包
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (photoPreviewPanel != null && photoPreviewPanel.activeSelf)
            {
                ClosePreview();
                Debug.Log("ClosePreview");
            }
            else
            {
                ClosePhotoAlbum();
                Debug.Log("ClosePhotoAlbums");
            }
        }
    }

    // 重新整理按鈕：只顯示已解鎖的，並把它們往前擠到最前面的格子
    public void RefreshPhotoButtons()
    {
        int previousIndex = _currentIndex; // 保留舊位置

        foreach (var btn in photoButtons)
        {
            if (btn == null)
                continue;
            btn.onClick.RemoveAllListeners();
            btn.gameObject.SetActive(false);

            var img = btn.GetComponent<Image>();
            if (img != null)
                img.sprite = null;
            if (img != null)
                img.color = normalColor;
        }

        _visibleButtons.Clear();

        int used = 0;
        for (int i = 0; i < allPhotos.Count; i++)
        {
            var data = allPhotos[i];
            if (!data.unlocked)
                continue;
            if (used >= photoButtons.Count)
                break;

            var btn = photoButtons[used];
            var img = btn.GetComponent<Image>();
            if (img != null)
                img.sprite = data.photo;

            int capturedId = data.id;
            int capturedIndex = used;
            btn.onClick.AddListener(() =>
            {
                _currentIndex = capturedIndex; // 記錄位置
                OpenPhoto(capturedId);
            });

            btn.gameObject.SetActive(true);
            _visibleButtons.Add(btn);
            used++;
        }

        // 若原位置超出範圍則歸零
        if (_visibleButtons.Count == 0)
            _currentIndex = -1;
        else if (previousIndex >= 0 && previousIndex < _visibleButtons.Count)
            _currentIndex = previousIndex;
        else
            _currentIndex = 0;

        UpdateHighlight();
    }

    // 更新選取高亮
    private void UpdateHighlight()
    {
        for (int i = 0; i < _visibleButtons.Count; i++)
        {
            var btn = _visibleButtons[i];
            var bg = btn.GetComponent<Image>();

            btn.transform.localScale = (i == _currentIndex) ? Vector3.one * 1.2f : Vector3.one;
        }

        if (_currentIndex >= 0 && _currentIndex < _visibleButtons.Count)
            EventSystem.current?.SetSelectedGameObject(_visibleButtons[_currentIndex].gameObject);
        else
            EventSystem.current?.SetSelectedGameObject(null);
    }

    // 透過「目前可見列表」索引，找回對應的 PhotoData（會用 id 對回資料）
    private PhotoData GetPhotoDataByVisibleIndex(int visibleIndex)
    {
        if (visibleIndex < 0 || visibleIndex >= _visibleButtons.Count)
            return null;

        // 取出該格子的圖來逆向找 PhotoData（更穩妥的是在 Refresh 時建立一個 index->id 對照）
        // 這裡改成在 Refresh 時就記錄「第 used 個格子對應哪個 Photo ID」，較精準。
        // 為了簡潔，這裡直接用顯示順序對回 allPhotos 中第 N 個 unlocked。
        int unlockedCountSeen = 0;
        for (int i = 0; i < allPhotos.Count; i++)
        {
            if (!allPhotos[i].unlocked)
                continue;
            if (unlockedCountSeen == visibleIndex)
                return allPhotos[i];
            unlockedCountSeen++;
        }
        return null;
    }

    // 開啟單張照片預覽
    public void OpenPhoto(int id)
    {
        var data = allPhotos.Find(p => p.id == id);
        if (data == null)
            return;

        if (previewImage != null)
            previewImage.sprite = data.photo;
        if (previewDescription != null)
            previewDescription.text = data.description;

        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(true);

        foreach (var btn in photoButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void ClosePreview()
    {
        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(false);

        RefreshPhotoButtons();
    }

    // 關閉相簿，回到背包
    public void ClosePhotoAlbum()
    {
        gameObject.SetActive(false);
        if (inventoryUI != null)
            inventoryUI.SetActive(true);
    }

    // 對外提供：解鎖照片（會立即存檔並刷新 UI）
    public void UnlockPhoto(int id, bool saveImmediately = true)
    {
        var data = allPhotos.Find(p => p.id == id);
        if (data != null && !data.unlocked)
        {
            data.unlocked = true;
            if (saveImmediately)
                SavePhotoAlbum();
            RefreshPhotoButtons();
        }
    }

    // ==== 存檔/讀檔 ====

    public void SavePhotoAlbum()
    {
        var save = new PhotoSaveData();
        foreach (var p in allPhotos)
        {
            if (p.unlocked)
                save.unlockedPhotoIDs.Add(p.id);
        }

        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(_savePath, json);
#if UNITY_EDITOR
        Debug.Log($"[PhotoAlbum] 已存檔：{_savePath}\n{json}");
#endif
    }

    public void LoadPhotoAlbum()
    {
        if (!File.Exists(_savePath))
        {
            // 第一次遊玩：用 Inspector 的初始 unlocked 狀態建立存檔
            SavePhotoAlbum();
#if UNITY_EDITOR
            Debug.Log("[PhotoAlbum] 存檔不存在，已用初始狀態建立。");
#endif
            return;
        }

        try
        {
            string json = File.ReadAllText(_savePath);
            var save = JsonUtility.FromJson<PhotoSaveData>(json);

            // 先全部鎖，再依照存檔解鎖
            var unlockedSet = new HashSet<int>(save.unlockedPhotoIDs);
            for (int i = 0; i < allPhotos.Count; i++)
            {
                allPhotos[i].unlocked = unlockedSet.Contains(allPhotos[i].id);
            }
#if UNITY_EDITOR
            Debug.Log($"[PhotoAlbum] 已讀檔：{_savePath}");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($"[PhotoAlbum] 讀檔失敗：{e.Message}\n將以初始狀態重建存檔。");
            SavePhotoAlbum();
        }
    }
}
