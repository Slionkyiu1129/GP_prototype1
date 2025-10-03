using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;  

public class checkFishCorrectManager : MonoBehaviour
{
    public checkFishCorrect[] fishes;  
    public GameObject successImage;

    private void Start()
    {
        if (saveManager.Instance.currentSave.fishPuzzleFinished)  // 改為 fishPuzzleFinished
        {
            foreach (checkFishCorrect fish in fishes)  // 改為遍歷 fish
            {
                fish.ForceSnapToCorrect();
            }

            successImage.SetActive(true);
        }
    }

    public void CheckAllFishes()  
    {
        foreach (checkFishCorrect fish in fishes)  // 改為遍歷 fish
        {
            if (!fish.IsCorrectlyPlaced())
                return;
        }
        successImage.SetActive(true);
        saveManager.Instance.currentSave.fishPuzzleFinished = true;  // 改為 fishPuzzleFinished
        DialogueManager.GetInstance().SetVariableState("fishPuzzleFinished", new StringValue("yes"));
        saveManager.Instance.SaveGame();
    }
}
