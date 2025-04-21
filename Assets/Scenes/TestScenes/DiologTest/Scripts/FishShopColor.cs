using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class FishShopColor : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color isfalse = Color.red;
    [SerializeField] private Color istrue = Color.green;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {

        string haveTalkFishman = ((Ink.Runtime.StringValue) DialogueManager
            .GetInstance()
            .GetVariableState("have_talk_fishman")).value;

        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null)
        {
            Debug.LogWarning("DialogueManager instance is null");
            return;
        }

        var variableState = dialogueManager.GetVariableState("have_talk_fishman");
        if (variableState == null)
        {
            Debug.LogWarning("Variable 'have_talk_fishman' not found");
            return;
        }

        // 檢查變數是否為 StringValue 類型
        if (!(variableState is Ink.Runtime.StringValue stringValue))
        {
            Debug.LogWarning($"Variable 'have_talk_fishman' is not a string. Current type: {variableState.GetType()}");
            return;
        }

        // Debug.Log("boolValue.value:" + boolValue.value);
        // spriteRenderer.color = boolValue.value ? istrue : isfalse;

        // string haveTalkFishman = stringValue.value;

        switch (haveTalkFishman)
        {
            case "":
                spriteRenderer.color = defaultColor;
                break;
            case "false":
                spriteRenderer.color = isfalse;
                break;
            case "true":
                spriteRenderer.color = istrue;
                break;
            default:
                Debug.LogWarning("Pokemon name not handled by switch statement: " + haveTalkFishman);
                break;
        }
    }
    // private void Update()
    // {
    //     var dialogueManager = DialogueManager.GetInstance();
    //     if (dialogueManager == null)
    //     {
    //         Debug.LogWarning("DialogueManager instance is null");
    //         return;
    //     }

    //     var variableState = dialogueManager.GetVariableState("haveTalkFishman");
    //     if (variableState == null)
    //     {
    //         Debug.LogWarning("Variable 'haveTalkFishman' not found");
    //         return;
    //     }

    //     string haveTalkFishman = ((Ink.Runtime.StringValue)variableState).value;

    //     switch (haveTalkFishman)
    //     {
    //         case "":
    //             spriteRenderer.color = defaultColor;
    //             break;
    //         case "false":
    //             spriteRenderer.color = isfalse;
    //             break;
    //         case "true":
    //             spriteRenderer.color = istrue;
    //             break;
    //         default:
    //             Debug.LogWarning("Unknown state value: " + haveTalkFishman);
    //             break;
    //     }
    // }
}
