using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Utils;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    public bool ManualFlip = false;
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float visionRange = 1.0f;
    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float seekPathInterval = 0.5f;
    [Header("Dash parameters")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float dashProbability;
    [SerializeField]
    private float dashRange;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldown;
    [Header("Patrol parameters")]
    [SerializeField]
    protected List<Vector2> movePositionsOffset = new List<Vector2>();

    protected int toSpot = 0;
    
    private Transform player;
    private Vector2 startPosition;
    private float dashDurationLeft;
    private float dashCooldownLeft;
    private bool isDashing = false;
    private Rigidbody2D enemyRigidbody;

    private Seeker seeker;
    private Path path;
    private int currentWayPoint = 0;
    private float seekerIntervalLeft = 0.0f;

    protected virtual void Start()
    {
        startPosition = transform.position;
        movePositionsOffset.Add(Vector2.zero);
        
        for(int i=0; i<movePositionsOffset.Count; i++)
        {
            movePositionsOffset[i] += startPosition;
        }

        dashDurationLeft = dashDuration;
        dashCooldownLeft = dashCooldown;
        seeker = GetComponent<Seeker>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        seeker.StartPath(transform.position, player.position, onPathComplete);
    }

    protected virtual void Update()
    {
        updateDash();
    }

    public Vector3 GetVectorToPlayer()
    {
        return player.position - transform.position;
    }

    public Vector3 GetDirectionToPlayer()
    {
        return GetVectorToPlayer().normalized;
    }

    public Vector3 GetHeadingDirection()
    {
        return enemyRigidbody.velocity.normalized;
    }

    public void Flip()
    {
        float xDirection = enemyRigidbody.velocity.x;
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
        enemyRigidbody.velocity = (GetNextPatrolPosition() - transform.position).normalized * speed;
    }

    public virtual Vector3 GetNextPatrolPosition()
    {
        updateToSpot();
        return startPosition + movePositionsOffset[toSpot-1];

    }

    protected virtual void updateToSpot()
    {
        if (Vector2.Distance(transform.position, movePositionsOffset[toSpot]) < minimumDistance)
        {
            toSpot += 1;

            if (toSpot >= movePositionsOffset.Count)
            {
                toSpot = 0;
            }
        }
    }

    public bool ShouldChase()
    {
        return GetVectorToPlayer().magnitude < visionRange;
    }

    public void Chase()
    {
        enemyRigidbody.velocity = (GetNextChasePosition() - transform.position).normalized * speed;
    }

    private Vector3 GetNextChasePosition()
    {
        updateSeekerInterval();
        return path.vectorPath[currentWayPoint];
    }

    private void updateSeekerInterval()
    {
        seekerIntervalLeft -= Time.deltaTime;
        if (seekerIntervalLeft < GrayConstants.MINIMUM_TIME)
        {
            seeker.StartPath(transform.position, player.position, onPathComplete);
        }

        if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < minimumDistance)
        {
            currentWayPoint += 1;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                seeker.StartPath(transform.position, player.position, onPathComplete);
            }
        }
    }

    private void onPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWayPoint = 0;
            seekerIntervalLeft = seekPathInterval;
        }
    }

    public bool IsReadyToDash()
    {
        float shouldDash = Random.Range(0.0f, 1.0f);
        return !isDashing && dashCooldownLeft <= 0.0f && shouldDash < dashProbability;
    }

    public void Dash()
    {
        isDashing = true;
        enemyRigidbody.velocity = GetVectorToPlayer().normalized * (dashRange/dashDuration);
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
            dashDurationLeft = dashDuration;
            dashCooldownLeft = dashCooldown;
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public void StopMoving()
    {
        enemyRigidbody.velocity = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, dashRange);
    }
}
