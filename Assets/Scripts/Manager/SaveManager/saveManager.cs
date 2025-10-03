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
            Debug.Log("檔案存到：" + savePath);
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
            Debug.Log("檔案存到：" + savePath);
        }
        else
        {
            currentSave = new saveData();
        }
    }

    //MARK: Specific Variables
    
    public void SaveLightState(string lightName, bool isLightOn, bool isFirstSpriteActive)
    {
        LightSaveData lightData = currentSave.lights.Find(l => l.lightName == lightName);
        if (lightData == null)
        {
            lightData = new LightSaveData();
            lightData.lightName = lightName;
            currentSave.lights.Add(lightData);
        }
        lightData.isLightOn = isLightOn;
        lightData.isFirstSpriteActive = isFirstSpriteActive;
    }

    public LightSaveData GetLightState(string lightName)
    {
        return currentSave.lights.Find(l => l.lightName == lightName);
    }


    public void SaveFishBagState(string fishBagName, Vector2 position)
    {
        FishBagItem fishBagData = currentSave.fishBags.Find(f => f.fishBagName == fishBagName);
        if (fishBagData == null)
        {
            fishBagData = new FishBagItem();
            fishBagData.fishBagName = fishBagName;
            currentSave.fishBags.Add(fishBagData);
        }
        fishBagData.position = position;
    }

    public FishBagItem GetFishBagState(string fishBagName)
    {
        return currentSave.fishBags.Find(f => f.fishBagName == fishBagName);
    }
}
