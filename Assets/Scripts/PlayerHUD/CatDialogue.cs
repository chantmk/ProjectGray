using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatDialogue 
{
    public bool isWait = false;
    public float timeOut = -1.0f;
    public CatSentence[] catSentences;
}
