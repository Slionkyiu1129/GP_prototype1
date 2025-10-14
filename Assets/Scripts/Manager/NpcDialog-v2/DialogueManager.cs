using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
// uning Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.06f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator nameAnimator;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator OpenCloseAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private DialogueVariables dialogueVariables;

    //Variables For FlyerNum to Inventory
    private bool hasAddedFlyer = false;
    private int lastFlyerNum = -1;
    private int flyerNum = 0;

    private bool autoDialogueMode = false;

    public void SetAutoDialogueMode(bool isAuto)
    {
        autoDialogueMode = isAuto;
    }

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
        //Get Flyer to InventorySystem
        FlyerNumToInventory();

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
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        // Check if the FlyerNum is Changed
        CheckFlyerNumChanged();
        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        // if (currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        // {
        //     ContinueStory();
        // }
        
        // if (autoDialogueMode && canContinueToNextLine && currentStory.currentChoices.Count == 0)
        // {
        //     ContinueStory();
        //     return;
        // }

        if (canContinueToNextLine
            && currentStory.currentChoices.Count == 0
            && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
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
        //OpenCloseAnimator.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);
        

        OpenCloseAnimator.GetComponent<PlayableDirector>().Play();
        
        yield return new WaitForSeconds(0.25f);
        autoDialogueMode = false; 
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        dialogueText.text = "";
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.text = line;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;

        if (autoDialogueMode && currentStory.currentChoices.Count == 0)
        {
            yield return new WaitForSeconds(1.5f);
            ContinueStory();
        }
    }

    private void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            //dialogueText.text = currentStory.Continue();

            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            // display choices, if any, for this dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            // handle tags
            HandleTags(currentStory.currentTags);
            //Get Flyer to InventorySystem
            FlyerNumToInventory();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
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
                    nameAnimator.Play(tagValue);
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
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
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

    public void SetVariableState(string variableName, Ink.Runtime.Object value)
    {
        

        // 如果有進行中的對話，同時更新故事變數
        if (currentStory != null)
        {
            currentStory.variablesState[variableName] = value;
        }
        
        // 無論是否有進行中的對話，都更新 dialogueVariables
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables[variableName] = value;
        }
        else
        {
            // 檢查這個變數是否在預設變數中存在
            if (dialogueVariables.variables.ContainsKey(variableName))
            {
                dialogueVariables.variables[variableName] = value;
            }
            else
            {
                dialogueVariables.variables.Add(variableName, value);
            }
        }
        
        // 同時保存變數狀態
        dialogueVariables.SaveVariables();
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }

    //MARK: -----Fucntion For FlyerNum To Inventory-----
    // flyerNum to Inventory ---> ContinueStory() and Start()
    private void FlyerNumToInventory()
    {
        Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
        Ink.Runtime.Object hasGetFlyerObj = GetVariableState("haveGetFlyer");

        //Check if there is flyerNumObj and hasGetFlyerObj
        if (flyerNumObj == null || hasGetFlyerObj == null)
        {
            return;
        }
        int.TryParse(flyerNumObj.ToString(), out flyerNum);
        
        Debug.Log("Check1_hasAddedFlyer =" + hasAddedFlyer);
        
        bool shouldAddFlyer = (hasGetFlyerObj != null && hasGetFlyerObj.ToString().ToLower() == "true");

        if (flyerNum > 0 && shouldAddFlyer && !hasAddedFlyer)
        {
            InventoryManager.Instance.AddItem("flyer", flyerNum);
            hasAddedFlyer = true;
            Debug.Log("Check2_hasAddedFlyer =" + hasAddedFlyer);
        }
    }

    // Check if the FlyerNum is Changed ---> Update()
    private void CheckFlyerNumChanged()
    {
        Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
        if (flyerNumObj == null) return;

        int currentFlyerNum = GetFlyerNum();
        if (currentFlyerNum != lastFlyerNum)
        {
            Debug.Log("Update Change FlyerNum" + currentFlyerNum);
            InventoryManager.Instance.UpdateFlyerAmount(currentFlyerNum);
            lastFlyerNum = currentFlyerNum;
        }
    }

    // Get flyer Num
    public int GetFlyerNum()
    {
        Ink.Runtime.Object flyerNumObj = GetVariableState("flyerNum");
        if (flyerNumObj != null && int.TryParse(flyerNumObj.ToString(), out int flyerNum))
        {
            return flyerNum;
        }
        return 0;
    }

    // Change flyer Num (delta can be -1 or +1)
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
    //-----End-----
}
