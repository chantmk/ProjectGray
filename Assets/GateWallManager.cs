using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWallManager : MonoBehaviour
{
    private Collider2D leftGateCollider;
    private Collider2D rightGateCollider;
    void Start()
    {
        leftGateCollider = GameObject.Find("LeftGate").GetComponent<Collider2D>();
        rightGateCollider = GameObject.Find("RightGate").GetComponent<Collider2D>();
        EventPublisher.GateEnable += GateEnable;
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventPublisher.GateEnable -= GateEnable;
    }

    public void GateEnable(bool enable)
    {
        //Open / Close Gate
        Debug.Log("Sth is calling gate " + enable);
        leftGateCollider.enabled = enable;
        rightGateCollider.enabled = enable;
    }
}
