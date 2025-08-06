using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhotoData
{
    public int id;
    public Sprite photo;
    [TextArea]
    public string description;
    public bool unlocked;
}
