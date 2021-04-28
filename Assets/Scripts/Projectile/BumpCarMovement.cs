using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCarMovement : EnemyMovement
{
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
        MovePositions = randomPosition();
    }

    protected override void Update()
    {
        base.Update();
        duration -= Time.fixedDeltaTime;
        if (duration <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private Vector2[] randomPosition()
    {
        Vector2[] positions = new Vector2[Random.Range(MinPositionCount, MaxPositionCount)];
        for (int i = 0; i < positions.Length; i++)
        {
            //positions[i] = new Vector2(Random.Range(BottomLeftCorner.x, TopRightCorner.x), Random.Range(BottomLeftCorner.y, TopRightCorner.y));
            positions[i].x = Random.Range(BottomLeftCorner.x, TopRightCorner.x);
            positions[i].y = Random.Range(BottomLeftCorner.y, TopRightCorner.y);
        }
        return positions;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(BottomLeftCorner, 0.2f);
        Gizmos.DrawSphere(TopRightCorner, 0.2f);
    }
}
