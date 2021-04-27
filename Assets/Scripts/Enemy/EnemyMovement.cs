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
    [Header("Enemy dash parameters")]
    [Range(0.0f, 1.0f)]
    public float DashProbability;
    public float DashForce;
    public float DashDuration;
    public float DashCooldown;
    [Header("Enemy patrol parameters")]
    public Vector2[] MovePositions = new Vector2[1];
    
    protected int toSpot = 0;
    private float dashDurationLeft;
    private float dashCooldownLeft;
    private bool isDashing = false;
    private Rigidbody2D rigidbody2D;

    protected virtual void Start()
    {
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
        dashDurationLeft = DashDuration;
        dashCooldownLeft = DashCooldown;
        rigidbody2D = GetComponent<Rigidbody2D>();
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

    // We will try to replace this with animator via EnemyBehaviour
    /*
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
        }*/

    public virtual Vector2 GetNextPatrolPosition()
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

    public bool IsReadyToDash()
    {
        float shouldDash = Random.Range(0.0f, 1.0f);
        return !isDashing && dashCooldownLeft <= 0.0f && shouldDash < DashProbability;
    }

    public void Dash()
    {
        Debug.Log("Dash " + rigidbody2D.velocity);
        isDashing = true;
        rigidbody2D.velocity = new Vector2(1.0f, 0.0f) * DashForce;
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
        }
        else if (dashDurationLeft <= 0.0f)
        {
            isDashing = false;
            rigidbody2D.velocity = Vector2.zero;
            dashDurationLeft = DashDuration;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
    }
}
