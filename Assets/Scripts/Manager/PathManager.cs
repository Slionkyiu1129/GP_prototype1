using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public string entryPointDirection;
    public List<string> playerPath = new List<string>(); // 紀錄出口順序
    public string[] correctPath = new string[] { "Up", "Down", "Left", "Right" }; // 正確順序

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDirection(string direction)
    {
        playerPath.Add(direction);

        // 判斷是否出錯
        if (!IsCurrentPathCorrect())
        {
            Debug.Log("順序錯誤，重來！");
            playerPath.Clear();
            // TODO: 讓玩家重來
        }
        else if (playerPath.Count == correctPath.Length)
        {
            Debug.Log("解謎成功！");
            // TODO: 成功邏輯
        }
    }

    private bool IsCurrentPathCorrect()
    {
        for (int i = 0; i < playerPath.Count; i++)
        {
            if (playerPath[i] != correctPath[i])
                return false;
        }
        return true;
    }

    public void ResetPath()
    {
        playerPath.Clear();
    }
}
