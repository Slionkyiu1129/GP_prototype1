using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGameManager : MonoBehaviour
{
    public void continueGame()
    {
        string nextScene = saveManager.Instance.currentSave.nextScene;
        SceneManager.LoadScene(nextScene);
    }

    public void newGame()
    {
        saveManager.Instance.currentSave = new saveData();
        saveManager.Instance.SaveGame();
        PlayerPrefs.DeleteAll();

        saveManager.Instance.currentSave.nextScene = "Scenes/Opening/BackMountain";
        SceneManager.LoadScene("Scenes/Opening/BackMountain");
    }
}
