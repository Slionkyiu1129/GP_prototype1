using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine;


public class DialogManager : MonoBehaviour
{
    public Text diaglogText;
    public Button[] buttons;
    public GameObject dialogUI;
    public GameObject nextBtn;
    bool getNextBtn = false;

    Story story = null;

    void Start()
    {
        // 确保游戏开始时UI是隐藏的
        if (dialogUI != null)
        {
            dialogUI.SetActive(false);
            nextBtn.SetActive(false);
        }
        // 确保所有按钮也是隐藏的
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public bool StartDialog(TextAsset inkAsset)
    {
        if (story != null) return false;
        story = new Story(inkAsset.text);
        if (dialogUI != null)
        {
            dialogUI.SetActive(true);
            nextBtn.SetActive(true);
        }
        NextDialog();
        return true;
    }

    // public void NextDialog()
    // {
    //     if (story == null) return;
    //     if (!story.canContinue && story.currentChoices.Count == 0)
    //     {
    //         Debug.Log("Dialog End");
    //         story = null;
    //         if (dialogUI != null)
    //         {
    //             dialogUI.SetActive(false);
    //             nextBtn.SetActive(false);
    //         }
    //         return;
    //     }
    //     if (story.currentChoices.Count > 0) SetChoices();
    //     if (story.canContinue) diaglogText.text = story.Continue();
    // }

    //2----------
    public void NextDialog()
    {
        if (story == null) return;

        // 對話結束情況
        if (!story.canContinue && story.currentChoices.Count == 0)
        {
            Debug.Log("Dialog End");
            story = null;
            if (dialogUI != null)
            {
                dialogUI.SetActive(false);
                nextBtn.SetActive(false);
            }
            return;
        }

        // 有選項時，顯示選項並暫停
        if (story.currentChoices.Count > 0)
        {
            //NextDialog();
            SetChoices();
            return; // 不繼續對話
        }

        // 沒有選項時才繼續對話內容
        if (story.canContinue)
        {
            diaglogText.text = story.Continue();
        }
    }

    // public void NextDialog()
    // {
    //     if (story == null) return;

    //     // 結束對話處理
    //     if (!story.canContinue && story.currentChoices.Count == 0)
    //     {
    //         Debug.Log("Dialog End");
    //         story = null;
    //         if (dialogUI != null)
    //         {
    //             dialogUI.SetActive(false);
    //             nextBtn.SetActive(false);
    //         }
    //         return;
    //     }

    //     // 對話循環直到遇到選項或不能再 continue
    //     while (story.canContinue)
    //     {
    //         diaglogText.text = story.Continue();

    //         // 如果 Continue() 後出現選項，先暫停並顯示
    //         if (story.currentChoices.Count > 0)
    //         {
    //             SetChoices();
    //             return;
    //         }
    //     }

    //     // 如果不能繼續，但有選項（也許一開始就出現），顯示它們
    //     if (story.currentChoices.Count > 0)
    //     {
    //         SetChoices();
    //     }
    // }



    private void SetChoices()
    {
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text;
        }
    }

    public void MakeChoice(int index)
    {
        story.ChooseChoiceIndex(index);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        NextDialog();
    }

    // void Update()
    // {
    //     // 如果對話正在進行中，並且按下 Enter 鍵
    //     if (story != null && Input.GetKeyDown(KeyCode.Return))
    //     {
    //         // 當前沒有選項時才繼續對話，避免跳過選項
    //         if (story.currentChoices.Count == 0)
    //         {
    //             NextDialog();
    //         }
    //     }
    // }

}


