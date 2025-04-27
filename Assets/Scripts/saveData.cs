using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveData
{
    public bool potPuzzleFinished = false;
    public List<LightSaveData> lights = new List<LightSaveData>();
}

[System.Serializable]
public class LightSaveData
{
    public string lightName;
    public bool isLightOn;
    public bool isFirstSpriteActive;
}