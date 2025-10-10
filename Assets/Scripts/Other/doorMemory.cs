using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorMemory : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 如果單純換圖片用這個
    public Sprite openSprite;             // 開門後的圖片
    public string targetSceneName;        // 要傳送的場景名稱

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite;
        }

        // 延遲一秒再傳送場景（等動畫播完）
        Invoke(nameof(LoadTargetScene), 1f);
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
