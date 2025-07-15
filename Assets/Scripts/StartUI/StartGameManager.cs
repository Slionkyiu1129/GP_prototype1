using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGameManager : MonoBehaviour
{
    public void LoadOpeningScene()
    {
        SceneManager.LoadScene("Scenes/Opening/BackMountain");
    }
}
