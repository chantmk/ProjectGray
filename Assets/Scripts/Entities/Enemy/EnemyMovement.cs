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
    private Vector3 footOffset;
    [SerializeField]
    protected float speed = 1.0f;
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
    [Tooltip("Offset from start position")]
    [SerializeField]
    protected List<Vector2> movePositions = new List<Vector2>();

    protected int toSpot = 0;
    protected Rigidbody2D enemyRigidbody;
    private Transform player;
    private Vector2 startPosition;
    private float dashDurationLeft;
    private float dashCooldownLeft;
    private bool isDashing = false;
    private GameObject healthBar;

    private Seeker seeker;
    private Path path;
    private int currentWayPoint = 0;
    private float seekerIntervalLeft = 0.0f;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        startPosition = transform.position;
        SetMovementPosition();
        for (int i = 0; i < movePositions.Count; i++)
        {
            movePositions[i] += startPosition;
        }

        FindHealthBar();

        dashDurationLeft = dashDuration;
        dashCooldownLeft = dashCooldown;

        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        
        seeker = GetComponent<Seeker>();
        FindPath();
    }

    protected virtual void Update()
    {
        updateDash();
    }

    protected virtual void SetMovementPosition()
    {
        movePositions.Add(new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized);
        movePositions.Add(new Vector2(Random.Range(0, 1), Random.Range(0, 1)).normalized);
        movePositions.Add(Vector2.zero);
    }

    protected virtual void FindHealthBar()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("HealthBarContainer").gameObject;
        }
    }

    public Vector3 GetVectorToPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        return player.position - transform.position;
    }

    public Vector3 GetDirectionToPlayer()
    {
        return GetVectorToPlayer().normalized;
    }

    public Vector3 GetHeadingDirection()
    {
        if (enemyRigidbody == null)
        {
            enemyRigidbody = GetComponent<Rigidbody2D>();
        }
        return enemyRigidbody.velocity.normalized;
    }

    public void Flip()
    {
        float xDirection = GetHeadingDirection().x;
        Vector3 enemyScale = transform.localScale;
        float enemyX = enemyScale.x;
        if (xDirection < -0.01f)
        {
            enemyX = -Mathf.Abs(enemyX);
        }
        else if (xDirection > 0.01f)
        {
            enemyX = Mathf.Abs(enemyX);
        }
        transform.localScale = new Vector3(enemyX, enemyScale.y, enemyScale.z);
        if (healthBar != null)
        {
            healthBar.transform.localScale = enemyScale;
        }
    }

    public virtual void Patrol()
    {
        enemyRigidbody.velocity = (GetNextPatrolPosition() - transform.position).normalized * speed;
    }

    public virtual Vector3 GetNextPatrolPosition()
    {
        updateToSpot();
        return movePositions[toSpot];

    }

    protected virtual void updateToSpot()
    {
        if (Vector2.Distance(transform.position, movePositions[toSpot]) < minimumDistance)
        {
            toSpot += 1;

            if (toSpot >= movePositions.Count)
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
        enemyRigidbody.velocity = (GetNextChasePosition() - (transform.position + footOffset)).normalized * speed;
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
            FindPath();
        }

        if (Vector3.Distance(transform.position + footOffset, path.vectorPath[currentWayPoint]) < minimumDistance)
        {
            if (currentWayPoint < path.vectorPath.Count-1)
            {
                currentWayPoint += 1;
            }
            else
            {
                FindPath();
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

    private void FindPath()
    {
        seeker.StartPath(transform.position + footOffset, player.position, onPathComplete); 
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

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + footOffset);
        Gizmos.DrawWireCube(transform.position + footOffset, new Vector3(0.5f, 0.2f, 0.2f));
    }
}
