﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCarMovement : EnemyMovement
{
    [Header("Bouncy")]
    [SerializeField]
    private LayerMask wall;
    [Header("Fix position")]
    [SerializeField]
    private int MaxPositionCount = 8;
    [SerializeField]
    private int MinPositionCount = 5;
    [SerializeField]
    private Vector2 BottomLeftCorner;
    [SerializeField]
    private Vector2 TopRightCorner;
    [SerializeField]
    private float duration = 10.0f;

    protected override void Start()
    {
        base.Start();
        movePositionsOffset = randomPosition();
    }

    protected override void Update()
    {
        base.Update();
        Destroy(gameObject, duration);
    }

    private List<Vector2> randomPosition()
    {
        List<Vector2> positions = new List<Vector2>();
        int size = Random.Range(MinPositionCount, MaxPositionCount);
        for (int i = 0; i < size; i++)
        {
            positions.Add(new Vector2(Random.Range(BottomLeftCorner.x, TopRightCorner.x), Random.Range(BottomLeftCorner.y, TopRightCorner.y)));
        }
        return positions;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        Debug.Log(collision.transform.position - transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(BottomLeftCorner, 0.2f);
        Gizmos.DrawSphere(TopRightCorner, 0.2f);
    }
}
