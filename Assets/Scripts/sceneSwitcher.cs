using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitcher : MonoBehaviour
{
    public string sceneName = "GalleryScene"; // �ؼг����W��
    public Transform spawnPoint; // �i�J�����᪺�X���I

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �s�x��e�����W��
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);

            // �p�G�����w�ǰe�I�A�O��s��m
            if (spawnPoint != null)
            {
                PlayerPrefs.SetFloat("LastX", spawnPoint.position.x);
                PlayerPrefs.SetFloat("LastY", spawnPoint.position.y);
            }

            PlayerPrefs.Save(); // �x�s�ƾ�
            SceneManager.LoadScene(sceneName); // ��������
        }
    }
}
