using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogeText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private GameObject speakerPortrait;
    [SerializeField] private Animator speakerPortraitAnimator;

    private Story currentStory;
    public bool playing { get; private set; }
    public bool done { get; private set; }
    //dialogue keys
    private const string SPEAKER = "speaker";

    private static DialogueManager instance;

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
    }

    private void Update()
    {
        if (!playing)   //return if no dialogue playing
        {
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
                    speakerNameText.text = tagValue;
                    speakerPortrait.SetActive(true);
                    speakerPortraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag detected but not handled" + tag);
                    break;
            }
        }

    }

}

