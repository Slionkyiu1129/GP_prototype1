using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PasswordTrigger : MonoBehaviour
{
    public PasswordUI passwordUI;
    private bool isPlayerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = true;
            Debug.Log("玩家靠近出口，按 Enter 開啟輸入介面");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = false;
            Debug.Log("玩家離開出口");
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            var save = saveManager.Instance.currentSave;

            if (save.hasKey1 && save.hasKey2)
            {
                if (!passwordUI.panel.activeSelf)
                {
                    passwordUI.Open();
                }
            }
            else
            {
                Debug.Log("需要兩支鑰匙才能開啟輸入介面！");
                // TODO: 這裡可以呼叫 UI 提示玩家
            }
        }
    }
}
