using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkVaseCorrectManager : MonoBehaviour
{
    public checkVaseCorrect[] pots; // �b Inspector ��i�h�Ҧ����
    public GameObject successImage; // ��ܪ��Ϥ�

    private void Start()
    {
        if (saveManager.Instance.currentSave.potPuzzleFinished)
        {
            foreach (checkVaseCorrect pot in pots)
            {
                pot.ForceSnapToCorrect(); // �A�ۤv�g�@�Ӥ�k����֦ۤv���h���T��m
            }

            successImage.SetActive(true);
        }
    }
    public void CheckAllPots()
    {
        foreach (checkVaseCorrect pot in pots)
        {
            if (!pot.IsCorrectlyPlaced())
                return; // ���@�Ӥ����T�N������
        }
        
        // ���������T
        successImage.SetActive(true);
        // �s��
        saveManager.Instance.currentSave.potPuzzleFinished = true;
        saveManager.Instance.SaveGame();
    }
}
