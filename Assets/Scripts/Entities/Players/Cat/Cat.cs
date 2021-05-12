using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Utils;

public class Cat : MonoBehaviour
{
    public enum EmotionEnum { Front, Back };
    public EmotionEnum emoState = EmotionEnum.Front;

    [SerializeField]
    private float minimumSpeed = 0.5f;
    [SerializeField]
    private float maximumSpeed = 2.5f;
    [SerializeField]
    private float minimumRadius = 2f;
    [SerializeField]
    private float maximumRadius = 10f;
    [SerializeField]
    private float minimumWalkTime = 3f;
    [SerializeField]
    private float maximumWalkTime = 5f;
    [SerializeField]
    private float minimumIdleTime = 0.2f;
    [SerializeField]
    private float maximumIdleTime = 2.0f;
    [SerializeField]
    private float maxSeekInterval = 0.5f;

    private Transform player;
    private Rigidbody2D catBody;
    private float currentWalkTimeLeft;
    private float currentIdleTimeLeft;
    private Seeker seeker;
    private Path path;
    private int currentWayPoint = 0;
    private float minimumOffset = 0.2f;
    private float seekerIntervalLeft = 0.0f;
    private bool isIdle = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        catBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        seekPath();
        currentWalkTimeLeft = Random.Range(minimumWalkTime, maximumWalkTime);
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            currentIdleTimeLeft -= Time.fixedDeltaTime;
            if (currentIdleTimeLeft < GrayConstants.MINIMUM_TIME)
            {
                resetWalkTime();
            }
        }
        else
        {
            currentWalkTimeLeft -= Time.fixedDeltaTime;
            if (currentWalkTimeLeft < GrayConstants.MINIMUM_TIME)
            {
                resetIdleTime();
            }
        }
    }

    public void DecideFollow()
    {
        float distance = getDistanceToPlayer();
        if (distance >= maximumRadius)
        {
            resetWalkTime();
        }
        else if (distance < minimumRadius)
        {
            resetIdleTime();
        }

        if (isIdle)
        {
            catBody.velocity = Vector2.zero;
        }
        else
        {
            followPlayer();
        }
    }

    public void followPlayer()
    {
        float distance = getDistanceToPlayer();
        if (distance < minimumRadius)
        {
            catBody.velocity = Vector2.zero;
        }
        else if (minimumRadius <= distance && distance < maximumRadius)
        {
            float ratio = (distance - minimumRadius) / maximumRadius;
            float calculatedSpeed = (minimumSpeed + maximumSpeed) * ratio;
            catBody.velocity = (GetNextPosition() - (Vector2)transform.position).normalized * calculatedSpeed;
        }
        else if (maximumRadius <= distance)
        {
            catBody.velocity = (GetNextPosition() - (Vector2)transform.position).normalized * maximumSpeed;
        }
        else
        {
            catBody.velocity = Vector2.zero;
        }
    }

    private Vector2 GetNextPosition()
    {
        updateSeekInterval();
        return path.vectorPath[currentWayPoint];
    }

    private void seekPath()
    {
        seeker.StartPath(transform.position, player.position, onPathComplete);
    }

    private void onPathComplete(Path newPath)
    {
        if (!newPath.error)
        {
            path = newPath;
            currentWayPoint = 0;
            seekerIntervalLeft = maxSeekInterval;
        }
    }

    private void updateSeekInterval()
    {
        seekerIntervalLeft -= Time.deltaTime;
        if (seekerIntervalLeft < GrayConstants.MINIMUM_TIME)
        {
            seekPath();
        }
        if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < minimumOffset)
        {
            if (currentWayPoint < path.vectorPath.Count - 1)
            {
                currentWayPoint += 1;
            }
            else
            {
                seekPath();
            }
        }
    }

    private Vector2 getVectorToPlayer()
    {
        return transform.position - player.position;
    }

    private float getDistanceToPlayer()
    {
        return getVectorToPlayer().magnitude;
    }

    private void resetWalkTime()
    {
        isIdle = false;
        currentWalkTimeLeft = Random.Range(minimumWalkTime, maximumWalkTime);
    }

    private void resetIdleTime()
    {
        isIdle = true;
        currentIdleTimeLeft = Random.Range(minimumIdleTime, maximumIdleTime);
        float emotion = Random.Range(0, 2);
        if (emotion == 0)
        {
            emoState = EmotionEnum.Front;
        }
        else
        {
            emoState = EmotionEnum.Back;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, maximumRadius);
        Gizmos.DrawWireSphere(transform.position, minimumRadius);
    }
}
