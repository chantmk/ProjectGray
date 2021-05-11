using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossMovement : EnemyMovement
{

    [SerializeField]
    private int[] spotCap = new int[1];

    private int spotCapPointer = 0;

    protected override void Start()
    {
        base.Start();
        spotCap[0] = movePositionsOffset.Count;
    }

    public void SetSpotCap(BossAggroEnum bossStatus)
    {
        switch(bossStatus)
        {
            case BossAggroEnum.Calm:
                spotCapPointer = 0;
                break;
            case BossAggroEnum.Enrage:
                if (spotCap.Length >= 1)
                {
                    spotCapPointer = 1;
                }
                break;
            case BossAggroEnum.Hyper:
                if (spotCap.Length >= 2)
                {
                    spotCapPointer = 2;
                }
                break;
            case BossAggroEnum.LastStand:
                spotCapPointer = 0;
                break;
        }
        spotCapPointer += 1;
        if (spotCapPointer >= spotCap.Length)
        {
            spotCapPointer = 0;
        }
    }
}
