using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogeText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private GameObject speakerPortrait;
    [SerializeField] private Animator speakerPortraitAnimator;
    [SerializeField] private GameObject portraitFrame;
    [SerializeField] private GameObject speakerFrame;
    [SerializeField] public bool isDialogueScene = false;
    [SerializeField] public LevelLoader loader;


    private Story currentStory;
    public bool playing { get; private set; }
    public bool done { get; private set; }
    // dialogue keys
    private const string SPEAKER = "speaker";
    private static DialogueManager instance;

    // character colours
    Color32 suzy_colour;
    Color32 cat_colour;
    Color32 description_colour;
    Color32 king_boss_colour;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        playing = false;
        done = false;
        dialoguePanel.SetActive(false);

        // setting colours
        suzy_colour = new Color32(69,190,141,255); // RGBA
        cat_colour = new Color32(255,173,0,255); // RGBA
        description_colour = new Color32(255,255,255,255); // RGBA

        king_boss_colour = new Color32(255,0,51,255); // RGBA
    }

    private void Update()
    {
        if (!playing)   // return if no dialogue playing
        {   
            if (isDialogueScene) { // if its a dialogue scene load next scene
                loader.LoadNextLevel();
            }
            return;
        }

        if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Dash"))
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        playing = true;
        dialoguePanel.SetActive(true);
        ContinueStory();

    }

    private void ExitDialogueMode()
    {
        playing = false;
        dialoguePanel.SetActive(false);
        dialogeText.text = "";
        done = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogeText.text = currentStory.Continue();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }

    }

    private void HandleTags(List<string> currentTags)
    {
        //Loop through each tag
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag was not appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handle the tag

            switch (tagKey)
            {
                case SPEAKER:
                    if ((tagValue) == "Cat" || (tagValue) == "Suzy" || (tagValue) == "King") { 
                        speakerNameText.text = tagValue;
                        portraitFrame.SetActive(true);
                        speakerFrame.SetActive(true);

                        speakerPortrait.SetActive(true);
                        speakerPortraitAnimator.Play(tagValue);

                        if ((tagValue) == "Cat") {
                            dialogeText.color = cat_colour;
                            speakerNameText.color = cat_colour;  
                        }
                        else if ((tagValue) == "Suzy") {
                            dialogeText.color = suzy_colour; 
                            speakerNameText.color = suzy_colour;    
                        }
                        else {
                            dialogeText.color = king_boss_colour; 
                            speakerNameText.color = king_boss_colour; 
                        }
                    }

                    if ((tagValue) == "Description") { 
                        portraitFrame.SetActive(false);
                        speakerFrame.SetActive(false);
                        dialogeText.color = description_colour; 
                    }
                    break;
                default:
                    Debug.LogWarning("Tag detected but not handled" + tag);
                    break;
            }
        }

    }

}

