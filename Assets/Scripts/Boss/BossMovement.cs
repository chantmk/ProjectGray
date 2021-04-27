using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{

    [SerializeField]
    private int[] spotCap = new int[1];
    private int spotCapPointer = 0;

    protected override void Start()
    {
        base.Start();
        spotCap[0] = MovePositions.Length;
    }
    public override Vector2 GetNextPatrolPosition()
    {
        if (Vector2.Distance(transform.position, MovePositions[toSpot]) < 0.2f)
        {
            toSpot += 1;

            if (toSpot >= spotCap[spotCapPointer])
            {
                toSpot = 0;
            }
        }
        return MovePositions[toSpot];
    }

    public void SetSpotCap(BossStatus bossStatus)
    {
        switch(bossStatus)
        {
            case BossStatus.Calm:
                spotCapPointer = 0;
                break;
            case BossStatus.Enrage:
                if (spotCap.Length >= 1)
                {
                    spotCapPointer = 1;
                }
                break;
            case BossStatus.Hyper:
                if (spotCap.Length >= 2)
                {
                    spotCapPointer = 2;
                }
                break;
            case BossStatus.LastStand:
                spotCapPointer = 0;
                break;
        }
        spotCapPointer += 1;
        if (spotCapPointer >= spotCap.Length)
        {
            spotCapPointer = 0;
        }
        Debug.Log("Spot pointer: " + spotCapPointer);
    }
}
