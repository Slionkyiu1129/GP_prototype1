using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrectManager : MonoBehaviour
{
    public checkVaseCorrect[] pots; // 在 Inspector 拖進去所有花盆
    public GameObject successImage; // 顯示的圖片

    private void Start()
    {
        if (saveManager.Instance.currentSave.potPuzzleFinished)
        {
            foreach (checkVaseCorrect pot in pots)
            {
                pot.ForceSnapToCorrect(); // 你自己寫一個方法讓花盆自己跳去正確位置
            }

            successImage.SetActive(true);
        }
    }
    public void CheckAllPots()
    {
        foreach (checkVaseCorrect pot in pots)
        {
            if (!pot.IsCorrectlyPlaced())
                return; // 有一個不正確就不做事
        }
        
        // 全部都正確
        successImage.SetActive(true);
        // 存檔
        saveManager.Instance.currentSave.potPuzzleFinished = true;
        saveManager.Instance.SaveGame();
    }
}
