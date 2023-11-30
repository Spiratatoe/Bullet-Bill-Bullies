using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{

    [Header("Ink JSON")]
    [SerializeField] private TextAsset dialogueInkJSON;
    

    void Start()
    {
        if (dialogueInkJSON != null)
        {
            if (!DialogueManager.GetInstance().done)
            {
                if (!DialogueManager.GetInstance().playing)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogueInkJSON);
                }
            }
        }
    }
    void Update()
    {
        if (dialogueInkJSON != null)
        {
            if (!DialogueManager.GetInstance().done)
            {
                if (!DialogueManager.GetInstance().playing)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(dialogueInkJSON);
                }
            }
        }
    }
}
