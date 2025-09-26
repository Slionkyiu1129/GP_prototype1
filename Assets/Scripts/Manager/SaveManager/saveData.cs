using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveData
{
    public int playerPosition = 0;
    public string nextScene = "Scenes/Opening/BackMountain"; 
    
    public bool hasStartGame = false;
    public bool potPuzzleFinished = false;
    public bool fishPuzzleFinished = false;

    public bool hasKey1 = false;
    public bool hasKey2 = false;
    public List<LightSaveData> lights = new List<LightSaveData>();
}

[System.Serializable]
public class LightSaveData
{
    public string lightName;
    public bool isLightOn;
    public bool isFirstSpriteActive;
}
