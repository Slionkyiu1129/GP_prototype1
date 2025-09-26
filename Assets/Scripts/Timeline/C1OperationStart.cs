using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class C1OperationStart : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        bool hasStartGame = saveManager.Instance.currentSave.hasStartGame;
        if(!hasStartGame)
        {
            director.Play(); 
            saveManager.Instance.currentSave.hasStartGame = true;
            saveManager.Instance.SaveGame();
        }
    }
}
