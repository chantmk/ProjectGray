using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateManager : MonoBehaviour
{
    public GameObject gateup;

    public GameObject gatedown;

    private bool isEnter;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isEnter)
        {
            isEnter = true;
            gatedown.SetActive(true);
        }
    }

    public void FinishRoom()
    {
        gateup.SetActive(false);
    }
}
