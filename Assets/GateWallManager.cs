using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWallManager : MonoBehaviour
{
    private Collider2D leftGateCollider;
    private Collider2D rightGateCollider;
    private int enemyCount;

    private void Awake()
    {
        EventPublisher.EnemySpawn += ListenEnemySpawn;
        EventPublisher.EnemyDestroy += ListenEnemyDestroy;
        enemyCount = 0;
    }

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
        EventPublisher.EnemySpawn -= ListenEnemySpawn;
        EventPublisher.EnemyDestroy -= ListenEnemyDestroy;
    }

    private void ListenEnemyDestroy()
    {
        enemyCount -= 1;
        if (enemyCount <= 0)
        {
            EventPublisher.TriggerGateEnable(false);
        }
        print(enemyCount);
    }

    private void ListenEnemySpawn()
    {
        enemyCount += 1;
        print(enemyCount);
    }


    public void GateEnable(bool enable)
    {
        //Open / Close Gate
        Debug.Log("Sth is calling gate " + enable);
        leftGateCollider.enabled = enable;
        rightGateCollider.enabled = enable;
    }
}
