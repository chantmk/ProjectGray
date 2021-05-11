using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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
    private int collisionDamage = 1;

    private BossAggroEnum bossStatus;
    private EnemyStats enemyStats;
    private Vector2 headingDirection;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }
    protected override void Start()
    {
        base.Start();
        EventPublisher.StatusChange += SetBossStatus;
        EventPublisher.StatusChange += SelfDestruct;
    }

    protected override void Update()
    {
        base.Update();
        if (MaxIdleDelay > 0.0f)
        {
            MaxIdleDelay -= Time.fixedDeltaTime;
        }
    }

    private void OnDestroy()
    {
        EventPublisher.StatusChange -= SetBossStatus;
        EventPublisher.StatusChange -= SelfDestruct;
    }

    protected override void SetMovementPosition()
    {

    }

    public void SetBossStatus(BossAggroEnum status)
    {
        bossStatus = status;
    }


    public bool IsChase()
    {
        return bossStatus == BossAggroEnum.Hyper && MaxIdleDelay <= 0.0f;
    }

    public void Shoot()
    {
        headingDirection = -GetDirectionToPlayer();
    }

    public void Fly()
    {
        enemyRigidbody.velocity = headingDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyStats == null)
        {
            Debug.Log("Holy shit");
        }
        if(enemyStats.Status == StatusEnum.Dead)
        {
            LayerMask collisionLayer = collision.gameObject.layer;
            if (collisionLayer == LayerMask.NameToLayer("Boss"))
            {
                collision.gameObject.GetComponent<BossStats>().TakeCrashDamage(collisionDamage);
                StopMoving();
                enemyStats.Die();
            }
            else if (collisionLayer == LayerMask.NameToLayer("WallHitbox"))
            {
                StopMoving();
                enemyStats.Die();
            }
        }
    }

    public void SelfDestruct(BossAggroEnum aggroEnum)
    {
        if (aggroEnum == BossAggroEnum.LastStand)
        {
            Destroy(gameObject);
        }
    }
}
