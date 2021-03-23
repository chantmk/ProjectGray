using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy life parameters")]
    public float MaxHealth;
    public float Shield;
    public float ShieldFactor;
    [Header("Enemy movement parameters")]
    public Vector2[] MovePositions;
    public float Speed;
    [Header("Enemy attack parameters")]
    public float VisionRange;
    public float AttackRange;
    public float AttackSpeed;

    private float CurrentHealth;
    private bool isDead => CurrentHealth <= 0f;
    private int toSpot = 0;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        CurrentHealth = MaxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
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

    public void FlipToPlayer(float xDirection)
    {
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
}
