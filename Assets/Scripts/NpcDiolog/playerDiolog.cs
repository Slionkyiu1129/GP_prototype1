using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDiolog : MonoBehaviour
{
    private DialogManager dialogManager;

    private bool canTalk = false;
    private NpcDialog currentNpc = null; // 当前交互的NPC
    
    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        //Debug.Log(dialogManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (canTalk && currentNpc != null && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("尝试开始对话");
            Debug.Log(dialogManager);
            // Debug.Log(currentNpc.inkAsset);
            if (dialogManager != null && currentNpc.inkAsset != null)
            {
                dialogManager.StartDialog(currentNpc.inkAsset);
            }
        }
    }

    // 改用触发器检测
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            currentNpc = other.GetComponent<NpcDialog>();
            if (currentNpc != null && currentNpc.inkAsset != null)
            {
                canTalk = true;
                Debug.Log("进入NPC范围，按E键开始对话");
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Npc"))
        {
            canTalk = false;
            currentNpc = null;
            Debug.Log("离开NPC范围");
        }
    }
}
