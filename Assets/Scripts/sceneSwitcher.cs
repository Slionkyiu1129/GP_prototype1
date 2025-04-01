using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitcher : MonoBehaviour
{
    public string sceneName = "GalleryScene"; // 目標場景名稱
    public Transform spawnPoint; // 進入場景後的出生點

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 存儲當前場景名稱
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

            // 如果有指定傳送點，記住新位置
            if (spawnPoint != null)
            {
                PlayerPrefs.SetFloat("LastX", spawnPoint.position.x);
                PlayerPrefs.SetFloat("LastY", spawnPoint.position.y);
            }

            PlayerPrefs.Save(); // 儲存數據
            SceneManager.LoadScene(sceneName); // 切換場景
        }
    }
}
