using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public int keyID; // 1 或 2
    private bool isPlayerNearby = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = true;
            Debug.Log("玩家靠近鑰匙點，按 Enter 拿鑰匙");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = false;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            if (keyID == 1 && !saveManager.Instance.currentSave.hasKey1)
            {
                saveManager.Instance.currentSave.hasKey1 = true;
                saveManager.Instance.SaveGame();
                Debug.Log("獲得鑰匙 1");
            }
            else if (keyID == 2 && !saveManager.Instance.currentSave.hasKey2)
            {
                saveManager.Instance.currentSave.hasKey2 = true;
                saveManager.Instance.SaveGame();
                Debug.Log("獲得鑰匙 2");
            }
            else
            {
                Debug.Log("已經拿過這把鑰匙");
            }
        }
    }
}
