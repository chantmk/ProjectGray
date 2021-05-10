﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
    public string name;
    public Sprite ProfilePicture;
    [TextArea(3, 10)]
    public string sentence;
    public bool IsRight;
}