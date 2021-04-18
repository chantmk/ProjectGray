using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DecisionManager decisionManager;
    public Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: remove (For debugging)
        if (Input.GetKeyDown(KeyCode.N))
        {
            dialogueManager.DisplayNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            decisionManager.Mercy();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            decisionManager.Kill();
        }
    }

    public void SayHiOnClick()
    {
        Debug.Log("Hi");
    }
}
