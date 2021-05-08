using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Player reference")]
    public GameObject player;
    [Header("Enemy movement parameters")]
    public float Speed = 1.0f;
    public float VisionRange = 1.0f;
    public bool ManualFlip = false;
    [Header("Enemy dash parameters")]
    [Range(0.0f, 1.0f)]
    public float DashProbability;
    public float DashRange;
    public float DashDuration;
    public float DashCooldown;
    [Header("Enemy patrol parameters")]
    public Vector2[] MovePositions = new Vector2[1];

    protected int toSpot = 0;
    private float dashDurationLeft;
    private float dashCooldownLeft;
    private bool isDashing = false;
    private Rigidbody2D enemyRigidbody;

    protected virtual void Start()
    {
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
        dashDurationLeft = DashDuration;
        dashCooldownLeft = DashCooldown;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
    }

    protected virtual void Update()
    {
        updateDash();
    }

    public Vector2 GetVectorToPlayer()
    {
        return player.transform.position - transform.position;
    }

    public void FlipToPlayer()
    {
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

    public void Patrol()
    {
        enemyRigidbody.velocity = (GetNextPatrolPosition() - transform.position).normalized * Speed;
    }

    public virtual Vector3 GetNextPatrolPosition()
    {
        if (Vector3.Distance(transform.position, MovePositions[toSpot]) < 0.2f)
        {
            toSpot += 1;

            if (toSpot >= MovePositions.Length)
            {
                toSpot = 0;
            }
        }
        return MovePositions[toSpot];
    }

    public bool IsReadyToDash()
    {
        float shouldDash = Random.Range(0.0f, 1.0f);
        return !isDashing && dashCooldownLeft <= 0.0f && shouldDash < DashProbability;
    }

    public void Dash()
    {
        isDashing = true;
        enemyRigidbody.velocity = GetVectorToPlayer().normalized * (DashRange/DashDuration);
    }

    private void updateDash()
    {
        if (!isDashing && dashCooldownLeft > 0.0f)
        {
            dashCooldownLeft -= Time.fixedDeltaTime;
        }

        if (isDashing && dashDurationLeft > 0.0f)
        {
            dashDurationLeft -= Time.fixedDeltaTime;
            Dash();
        }
        else if (dashDurationLeft <= 0.0f)
        {
            isDashing = false;
            dashDurationLeft = DashDuration;
            dashCooldownLeft = DashCooldown;
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }
    public void Chase()
    {
        enemyRigidbody.velocity = GetVectorToPlayer().normalized * Speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, DashRange);
    }
}
