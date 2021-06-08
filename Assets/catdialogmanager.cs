using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class catdialogmanager : MonoBehaviour
{
    public TMP_Text mtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        mtext.SetText(text);
        Invoke("ResetText", 5f);
    }

    public void ResetText()
    {
        mtext.SetText("");
    }
}
