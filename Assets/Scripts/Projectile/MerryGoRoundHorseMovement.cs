using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerryGoRoundHorseMovement : EnemyMovement
{
    /**
     * The horse of merry go round
     * Will be creatd by merry go round
     * Idle: Circling around the center
     * Player can shoot to this horse only when it is in the front
     * Shot: flew as same as bullet direction and speed
     * Collide with boss: Deal damage to boss
     */
    [SerializeField]
    private float MaxIdleDelay = 10.0f;
    [SerializeField]
    private float MoveRadius = 10.0f;

    private Vector2 centerPosition;
    private BossStatus bossStatus;

    protected override void Start()
    {
        base.Start();
        centerPosition = transform.position;
        setPosition();

        EventPublisher.StatusChange += SetBossStatus;
    }
    protected override void Update()
    {
        base.Update();
        if (MaxIdleDelay > 0.0f)
        {
            MaxIdleDelay -= Time.fixedDeltaTime;
        }
    }

    public void SetBossStatus(BossStatus status)
    {
        bossStatus = status;
    }

    private void setPosition()
    {
        MovePositions = new Vector2[2];
        MovePositions[0].x = centerPosition.x;
        MovePositions[0].y = centerPosition.y + MoveRadius;
        MovePositions[1].x = centerPosition.x;
        MovePositions[1].y = centerPosition.y + MoveRadius;
    }

    public bool IsIdle()
    {
        return bossStatus == BossStatus.Hyper && MaxIdleDelay <= 0.0f;
    }
}
