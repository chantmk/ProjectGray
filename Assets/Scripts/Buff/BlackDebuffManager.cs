using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDebuffManager : MonoBehaviour
{
    private SpriteRenderer vignette;
    
    void Start()
    {
        vignette = GetComponent<SpriteRenderer>();
    }

    public void DebuffEnter()
    {
        vignette.enabled = true;
    }

    public void DebuffExit()
    {
        vignette.enabled = false;
    }
}
