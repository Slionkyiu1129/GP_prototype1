using TMPro;
using UnityEngine;

public class PasswordUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;
    public TextMeshProUGUI[] digitTexts;
    public CharacterController2D_addGrid player;

    private int[] digits = new int[4];
    private int currentIndex = 0;
    private string correctPassword = "5219";

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (!panel.activeSelf)
            return;

        // 左右切換位數
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentIndex = Mathf.Max(0, currentIndex - 1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentIndex = Mathf.Min(3, currentIndex + 1);

        // 上下改變數字
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            digits[currentIndex] = (digits[currentIndex] + 1) % 10;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            digits[currentIndex] = (digits[currentIndex] + 9) % 10;

        UpdateUI();

        // 按 Enter 檢查密碼
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = "";
            foreach (int d in digits)
                input += d.ToString();
            Debug.Log("輸入密碼：" + input);
            if (input == correctPassword)
            {
                Debug.Log("密碼正確");
                panel.SetActive(false);
                // TODO: 在這裡放傳送場景或換圖片
            }
            else
            {
                Debug.Log("密碼錯誤");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Close();
        }
    }

    // 開啟輸入面板
    public void Open()
    {
        for (int i = 0; i < 4; i++)
            digits[i] = 0; // 初始化
        currentIndex = 0;
        panel.SetActive(true);
        UpdateUI();
        if (player != null)
            player.enabled = false;
    }

    public void Close()
    {
        panel.SetActive(false);
        if (player != null)
            player.enabled = true;
    }

    // 更新 UI 顯示
    void UpdateUI()
    {
        for (int i = 0; i < 4; i++)
        {
            digitTexts[i].text = digits[i].ToString();
            digitTexts[i].color = (i == currentIndex) ? Color.yellow : Color.black;
        }
    }
}
