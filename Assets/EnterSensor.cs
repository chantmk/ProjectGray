using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnterSensor : MonoBehaviour
{
    private bool isEntered;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEntered && other.CompareTag("Player"))
        {
            Debug.Log(other + " enter");
            EventPublisher.TriggerGateEnable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (!isEntered && other.CompareTag("Player"))
        {
            Debug.Log(other + " exit");
            isEntered = true;
        }
    }
}
