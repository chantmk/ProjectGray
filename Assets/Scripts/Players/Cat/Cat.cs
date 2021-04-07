using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float MaximumRadius = 10f;
    public float MinimumRadius = 2f;
    public float MaximumWalkTime = 5f;
    public float MinimumWalkTime = 3f;
    public Transform player;

    private Vector3 nextPostion;
    private float currentTimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        currentTimeLeft = Random.Range(MinimumWalkTime, MaximumWalkTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, MaximumRadius);
        Gizmos.DrawWireSphere(transform.position, MinimumRadius);
    }

    public Vector3 calculateNextPosition()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= MinimumRadius || distance >= MaximumRadius || currentTimeLeft < 0.01f)
        {
            // Random position, time and start walking by save new position
            currentTimeLeft = Random.Range(MinimumWalkTime, MaximumWalkTime);
            float randomX = Random.Range(MinimumRadius, MaximumRadius);
            float randomY = Random.Range(MinimumRadius, MaximumRadius);
            nextPostion = new Vector3(randomX, randomY);
        }
        else
        {
            // count time
            currentTimeLeft -= Time.fixedDeltaTime;
        }
        return nextPostion;
    }
}
