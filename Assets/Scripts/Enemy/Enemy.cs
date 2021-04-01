using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public Transform player;
    [Header("Enemy life parameters")]
    public float MaxHealth;
    public float Shield;
    public float ShieldFactor;
    [Header("Enemy movement parameters")]
    public Vector2[] MovePositions = new Vector2[1];
    public float Speed;
    [Header("Enemy attack parameters")]
    public float VisionRange;
    public float AttackRange;
    public float AttackSpeed;
    public bool IsRange;

    private float CurrentHealth;
    private bool isDead => CurrentHealth <= 0f;
    private int toSpot = 0;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;


    public float GetPercentHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            CurrentHealth -= (damage - ShieldFactor * Shield);

            if (isDead)
            {
                CurrentHealth = 0f;
            }
        }
    }

    public Vector2 GetVectorToPlayer()
    {
        return player.position - transform.position;
    }

    public void FlipToPlayer()
    {
        //float xDirection = player.position.x - transform.position.x;
        float xDirection = GetVectorToPlayer().x;
        float enemyX = transform.localScale.x;
        if (xDirection < -0.01f)
        {
            enemyX = -Mathf.Abs(enemyX);
        }
        else if (xDirection > 0.01f)
        {
            enemyX = Mathf.Abs(enemyX);
        }
        transform.localScale = new Vector3(enemyX, transform.localScale.y, transform.localScale.z);
    }

    public Vector2 GetNextPatrolPosition()
    {
        if (Vector2.Distance(transform.position, MovePositions[toSpot]) < 0.2f)
        {
            toSpot += 1;

            if (toSpot >= MovePositions.Length)
            {
                toSpot = 0;
            }
        }
        return MovePositions[toSpot];
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
    }
}
