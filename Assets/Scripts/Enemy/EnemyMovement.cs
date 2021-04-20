using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Player reference")]
    public GameObject player;
    [Header("Enemy movement parameters")]
    public Vector2[] MovePositions = new Vector2[1];
    public float Speed = 1.0f;
    public float VisionRange = 1.0f;

    private int toSpot = 0;

    protected virtual void Start()
    {
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
    }
}
