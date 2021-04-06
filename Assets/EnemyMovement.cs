using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2[] MovePositions = new Vector2[1];
    public float Speed;

    private float CurrentHealth;
    private bool isDead => CurrentHealth <= 0f;
    private int toSpot = 0;

    private HashSet<Collider2D> visionColliders;

    void Start()
    {
        MovePositions[0] = new Vector2(transform.position.x, transform.position.y);
        visionColliders = transform.Find("VisionHitbox").GetComponent<HitboxCollider>().HitColliders;
    }

    public void Flip()
    {

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

    public bool FollowPlayer()
    {
        foreach (Collider2D collider in visionColliders)
        {
            //if (collider.TryGetComponent)
            //{
            //    // return transform of player that should follow if there is not player then patrol
            //}
        }
        return true
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
