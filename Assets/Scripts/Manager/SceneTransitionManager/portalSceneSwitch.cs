using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSceneSwitch : MonoBehaviour
{
    public string targetScene;
    public int targetSpawnIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter: " + other.name + ", tag: " + other.tag);
        if (other.CompareTag("player"))
        {
            saveManager.Instance.currentSave.playerPosition = targetSpawnIndex;
            saveManager.Instance.currentSave.nextScene = targetScene;
            saveManager.Instance.SaveGame();
            SceneTransitionManager.Instance.SwitchScene(targetScene);
        }
    }
}
