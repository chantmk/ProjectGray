using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BumpCarMovement : EnemyMovement
{
    [Header("Bouncy")]
    [SerializeField]
    private float bounceAngle = 60.0f;
    [SerializeField]
    private int collisionDamage = 1;
    [SerializeField]
    private float consumeTime = 10.0f;
    private Vector3 currentHeading;
    private EnemyStats enemyStats;

    protected override void Start()
    {
        base.Start();
        enemyStats = GetComponent<EnemyStats>();
        EventPublisher.StatusChange += SelfDestruct;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        EventPublisher.StatusChange -= SelfDestruct;
    }

    protected void FixedUpdate()
    {
        if (enemyStats.Status == StatusEnum.Dead)
        {
            consumeTime -= Time.fixedDeltaTime;
            if (consumeTime < GrayConstants.MINIMUM_TIME)
            {
                enemyStats.Die();
            }
        }
    }

    public override void Patrol()
    {
        enemyRigidbody.velocity = currentHeading;
    }

    public void Shoot()
    {
        currentHeading = -GetDirectionToPlayer() * speed;
    }

    private void RandomHeading(Collider2D collision)
    {
        Vector3 direction = (transform.position - (Vector3)collision.ClosestPoint(transform.position)).normalized;
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-bounceAngle, bounceAngle), Vector3.up);
        currentHeading = rotation * direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask collisionLayer = collision.gameObject.layer;
        if (collisionLayer == LayerMask.NameToLayer("WallHitbox"))
        {
            RandomHeading(collision);
        }
        else if (collisionLayer == LayerMask.NameToLayer("Player") && enemyStats.Status != StatusEnum.Dead)
        {
            RandomHeading(collision);
            collision.gameObject.GetComponent<CharacterStats>().TakeDamage(collisionDamage);
        }
        else if (collisionLayer == LayerMask.NameToLayer("Boss") && enemyStats.Status == StatusEnum.Dead)
        {
            Debug.Log("Crash");
            collision.gameObject.GetComponent<BossStats>().TakeCrashDamage(collisionDamage);
            enemyStats.Die();
        }
        else if (collisionLayer == LayerMask.NameToLayer("PlayerHitbox"))
        {
            Shoot();
        }
    }

    public void SelfDestruct(BossAggroEnum status)
    {
        if (status == BossAggroEnum.LastStand)
        {
            Destroy(gameObject);
        }
    }
}
