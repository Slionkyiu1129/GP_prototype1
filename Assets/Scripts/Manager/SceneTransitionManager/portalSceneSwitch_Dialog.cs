using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSceneSwitch_Dialog : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public string targetScenePath;   //e.g. "A/home" (資料夾/場景名稱)
    public int targetSpawnIndex;

    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter: " + other.name + ", tag: " + other.tag);
        if (other.CompareTag("player"))
        {
            Debug.Log("Starting Dialogue before Scene Switch");
            DialogueManager.GetInstance().SetSceneSwitchAfterDialog(true);
            DialogueManager.GetInstance().SetAutoDialogueMode(false);
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
    
    public void SwitchSceneAfterDialogue()
    {
        saveManager.Instance.currentSave.playerPosition = targetSpawnIndex;
        saveManager.Instance.currentSave.nextScene = "Scenes/" + targetScenePath;
        saveManager.Instance.SaveGame();
            
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            SceneTransitionManager.Instance.SwitchScene("Scenes/" + targetScenePath);
        }
    }
}
