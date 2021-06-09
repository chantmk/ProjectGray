using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class Sentence
{
    //public string name;
    public CharacterNameEnum name;
    public Sprite ProfilePicture;
    [TextArea(3, 10)]
    public string sentence;
    public bool IsRight;
}
