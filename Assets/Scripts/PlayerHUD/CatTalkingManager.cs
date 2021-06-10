using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTalkingManager : MonoBehaviour
{
    [Header("Dialogues")]
    public Dialogue[] dialogues;
    private CatDialogueManager catDialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        catDialogueManager = FindObjectOfType<CatDialogueManager>();
    }

    public void TriggerDialogue(int dialogueCount)
    {
        catDialogueManager.StartDialogue(dialogues[dialogueCount]);
    }
}
