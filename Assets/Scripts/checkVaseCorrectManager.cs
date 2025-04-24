using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrectManager : MonoBehaviour
{
    public checkVaseCorrect[] pots;
    public GameObject successImage;

    private void Start()
    {
        if (saveManager.Instance.currentSave.potPuzzleFinished)
        {
            foreach (checkVaseCorrect pot in pots)
            {
                pot.ForceSnapToCorrect();
            }

            successImage.SetActive(true);
        }
    }
    public void CheckAllPots()
    {
        foreach (checkVaseCorrect pot in pots)
        {
            if (!pot.IsCorrectlyPlaced())
                return;
        }
        successImage.SetActive(true);
        saveManager.Instance.currentSave.potPuzzleFinished = true;
        saveManager.Instance.SaveGame();
    }
}
