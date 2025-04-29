using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
// uning Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private DialogueVariables dialogueVariables;
    private bool hasAddedFlyer = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        // dialogueVariables = new DialogueVariables(loadGlobalsJSON.filePath);
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        InventoryManager.Instance.SyncFlyerFromDialogue();
        int flyerNum = 0;
            Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
            if (flyerNumObj != null)
            {
                int.TryParse(flyerNumObj.ToString(), out flyerNum);
            }

            InventoryManager inventory = InventoryManager.Instance;
        
            if (flyerNum > 0 && !hasAddedFlyer) 
            {
                inventory.AddItem("flyer", flyerNum); 
                hasAddedFlyer = true;
            }
            InventoryManager.Instance.SyncFlyerFromDialogue();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        InventoryManager.Instance.SyncFlyerFromDialogue();
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
            InventoryManager.Instance.SyncFlyerFromDialogue();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();
            // handle tags
            HandleTags(currentStory.currentTags);
            int flyerNum = 0;
            Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
            if (flyerNumObj != null)
            {
                int.TryParse(flyerNumObj.ToString(), out flyerNum);
            }

            InventoryManager inventory = InventoryManager.Instance;
        
            if (flyerNum > 0 && !hasAddedFlyer) 
            {
                inventory.AddItem("flyer", flyerNum); 
                hasAddedFlyer = true;
            }
            inventory.SyncFlyerFromDialogue();
        }
        else
        {
            InventoryManager.Instance.SyncFlyerFromDialogue();
            StartCoroutine(ExitDialogueMode());
        }
    }
    // 取得 flyer 數量
    public int GetFlyerNum()
    {
        Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
        if (flyerNumObj != null && int.TryParse(flyerNumObj.ToString(), out int flyerNum))
        {
            return flyerNum;
        }
        return 0;
    }

    // 改變 flyer 數量 (delta 可以是 +1 或 -1)
    public void ChangeFlyerNum(int delta)
    {
        if (currentStory != null)
        {
            int currentFlyerNum = GetFlyerNum();
            int newFlyerNum = currentFlyerNum + delta;
            currentStory.variablesState["flyerNum"] = newFlyerNum;

            // 加這一行！改完 Ink 的數量後馬上同步到 Inventory
            InventoryManager.Instance.UpdateFlyerAmount(newFlyerNum);
        }
    }
    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    //Debug.Log("speaker="+tagValue);
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    //Debug.Log("portrait="+tagValue);
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout=" + tagValue);
                    //layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }


    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }

}
