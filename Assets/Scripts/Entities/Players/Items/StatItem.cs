using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatItem : Interactable
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    
}
