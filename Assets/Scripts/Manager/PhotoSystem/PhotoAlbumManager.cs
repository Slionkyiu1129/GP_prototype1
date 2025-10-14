using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class PhotoDataJSON
{
    public int id;
    public string name;
    public string imagePath;
    public string description;
    public bool unlocked;
}

[Serializable]
public class PhotoAlbumJSON
{
    public List<PhotoDataJSON> photos = new List<PhotoDataJSON>();
}

public class PhotoAlbumManager : MonoBehaviour
{
    [Header("PhotoButtons")]
    public List<Button> photoButtons;

    [Header("UI")]
    public GameObject photoPreviewPanel;
    public Image previewImage;
    public TMP_Text previewDescription;
    public GameObject inventoryUI;

    [Header("Player")]
    public CharacterController2D_addGrid player;

    /*[Header("顏色")]
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.65f, 0.8f, 1f); */

    private List<PhotoDataJSON> allPhotos = new List<PhotoDataJSON>();
    private List<Button> _visibleButtons = new List<Button>();
    private int _currentIndex = -1;
    private string _savePath;
    private const int COLS = 4; // 相簿一列 4 張
    public bool IsAlbumActive =>
        gameObject.activeSelf || (photoPreviewPanel != null && photoPreviewPanel.activeSelf);

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "photoAlbum.json");
    }

    private void Start()
    {
        LoadPhotoAlbum();
        RefreshPhotoButtons();
        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(false);
    }

    /*
        [ContextMenu("Generate Initial JSON")]
        public void GenerateInitialJSON()
        {
            var album = new PhotoAlbumJSON();
            album.photos = new List<PhotoDataJSON>();
    
            for (int i = 1; i <= 9; i++) // 預設 9 張
            {
                album.photos.Add(
                    new PhotoDataJSON
                    {
                        id = i,
                        name = $"照片 {i}",
                        imagePath = $"Photos/photo{i}", // 對應 Resources/Photos/photo1.png ...
                        description = $"這是照片 {i} 的描述。",
                        unlocked = false,
                    }
                );
            }
    
            string json = JsonUtility.ToJson(album, true);
            File.WriteAllText(_savePath, json);
    
    #if UNITY_EDITOR
            Debug.Log($"✅ 已生成初始 JSON，路徑：{_savePath}\n{json}");
    #endif
        } */

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        bool hasAny = _visibleButtons.Count > 0;

        // --- 控制方向 ---
        if (hasAny && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            _currentIndex = Mathf.Max(_currentIndex - COLS, 0);
            UpdateHighlight();
        }
        else if (hasAny && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            _currentIndex = Mathf.Min(_currentIndex + COLS, _visibleButtons.Count - 1);
            UpdateHighlight();
        }
        else if (hasAny && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            _currentIndex = Mathf.Max(_currentIndex - 1, 0);
            UpdateHighlight();
        }
        else if (hasAny && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            _currentIndex = Mathf.Min(_currentIndex + 1, _visibleButtons.Count - 1);
            UpdateHighlight();
        }
        else if (
            hasAny && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        )
        {
            var data = GetPhotoDataByVisibleIndex(_currentIndex);
            if (data != null)
                OpenPhoto(data);
        }
        // --- 關閉介面 ---
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (photoPreviewPanel != null && photoPreviewPanel.activeSelf)
            {
                ClosePreview();
            }
            else
            {
                ClosePhotoAlbum();
            }
        }
    }

    // --- UI ---
    public void RefreshPhotoButtons()
    {
        foreach (var btn in photoButtons)
        {
            if (btn == null)
                continue;
            btn.onClick.RemoveAllListeners();
            btn.gameObject.SetActive(false);

            var img = btn.GetComponent<Image>();
            if (img != null)
                img.sprite = null;
        }

        _visibleButtons.Clear();
        int used = 0;

        foreach (var data in allPhotos)
        {
            if (!data.unlocked)
                continue;
            if (used >= photoButtons.Count)
                break;

            var btn = photoButtons[used];
            var img = btn.GetComponent<Image>();
            if (img != null)
                img.sprite = Resources.Load<Sprite>(data.imagePath);

            int capturedIndex = used;
            btn.onClick.AddListener(() =>
            {
                _currentIndex = capturedIndex;
                OpenPhoto(data);
            });

            btn.gameObject.SetActive(true);
            _visibleButtons.Add(btn);
            used++;
        }

        if (_currentIndex == -1 && _visibleButtons.Count > 0)
            _currentIndex = 0;

        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        for (int i = 0; i < _visibleButtons.Count; i++)
            _visibleButtons[i].transform.localScale =
                (i == _currentIndex) ? Vector3.one * 1.2f : Vector3.one;
    }

    private PhotoDataJSON GetPhotoDataByVisibleIndex(int index)
    {
        if (index < 0 || index >= _visibleButtons.Count)
            return null;
        return allPhotos.Find(p =>
            p.unlocked
            && _visibleButtons[index].GetComponent<Image>().sprite
                == Resources.Load<Sprite>(p.imagePath)
        );
    }

    public void OpenPhoto(PhotoDataJSON data)
    {
        if (previewImage != null)
            previewImage.sprite = Resources.Load<Sprite>(data.imagePath);
        if (previewDescription != null)
            previewDescription.text = data.description;

        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(true);

        foreach (var btn in photoButtons)
            btn.gameObject.SetActive(false);
    }

    public void ClosePreview()
    {
        if (photoPreviewPanel != null)
            photoPreviewPanel.SetActive(false);

        RefreshPhotoButtons();
        UpdateHighlight();
    }

    public void ClosePhotoAlbum()
    {
        gameObject.SetActive(false);
        if (inventoryUI != null)
            inventoryUI.SetActive(true);

        if (player != null)
            player.enabled = true;
    }

    // --- 存檔/讀檔 ---
    public void LoadPhotoAlbum()
    {
        if (!File.Exists(_savePath))
        {
            Debug.LogError("！！！[PhotoAlbum] JSON 檔不存在");
            return;
        }

        string json = File.ReadAllText(_savePath);
        var album = JsonUtility.FromJson<PhotoAlbumJSON>(json);
        allPhotos = album.photos;
        Debug.Log("照片檔案存在：" + _savePath);
    }

    public void SavePhotoAlbum()
    {
        var album = new PhotoAlbumJSON { photos = allPhotos };
        string json = JsonUtility.ToJson(album, true);
        File.WriteAllText(_savePath, json);
    }

    public void UnlockPhoto(int id)
    {
        var photo = allPhotos.Find(p => p.id == id);
        if (photo != null && !photo.unlocked)
        {
            photo.unlocked = true;
            SavePhotoAlbum();
            RefreshPhotoButtons();
        }
    }
}
