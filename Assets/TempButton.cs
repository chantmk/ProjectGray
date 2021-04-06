using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempButton : MonoBehaviour
{
    public DialogueManager dm;
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
            dm.DisplayNextSentence();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            boss.ChooseMercy();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            boss.ChooseKill();
        }
    }
}
