using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class saveManager : MonoBehaviour
{
    public static saveManager Instance;
    private string savePath;
    public saveData currentSave;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "save.json");
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentSave = JsonUtility.FromJson<saveData>(json);
            Debug.Log("Game Loaded");
        }
        else
        {
            currentSave = new saveData();
        }
    }
}
