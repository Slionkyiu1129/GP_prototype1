using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int spawnIndex = saveManager.Instance.currentSave.playerPosition;
        var positions = FindObjectOfType<playerPosition>();
        if (positions != null)
        {
            Transform spawnPoint = positions.GetSpawnPoint(spawnIndex);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && spawnPoint != null)
                player.transform.position = spawnPoint.position;
        }
    }

    public void SwitchScene(string targetScenePath)
    {
        SceneManager.LoadScene(targetScenePath);
    }
}
