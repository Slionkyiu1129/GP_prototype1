using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSceneSwitch : MonoBehaviour
{
    public string targetScenePath;   //e.g. "A/home" (資料夾/場景名稱)
    public int targetSpawnIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter: " + other.name + ", tag: " + other.tag);
        if (other.CompareTag("player"))
        {
            saveManager.Instance.currentSave.playerPosition = targetSpawnIndex;
            saveManager.Instance.currentSave.nextScene = "Scenes/"+targetScenePath;
            saveManager.Instance.SaveGame();
            SceneTransitionManager.Instance.SwitchScene("Scenes/"+targetScenePath);
        }
    }
}
