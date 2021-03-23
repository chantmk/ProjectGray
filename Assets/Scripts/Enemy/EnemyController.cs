using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum Status{ PATROL, CHASE, ATTACK};
    public float Speed;
    public float ViewRange;
    public float AttackRange;
    //public Vector2 StartPoint;

    private Transform target;
    private Status enemyStatus;
    
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = transform.position.Set(StartPoint);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();
        if(enemyStatus == Status.CHASE)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
    }

    void CheckRange()
    {
        if (Vector2.Distance(transform.position, target.position) < AttackRange)
        {
            enemyStatus = Status.ATTACK;
        }
        else if (Vector2.Distance(transform.position, target.position) < ViewRange)
        {
            enemyStatus = Status.CHASE;
        }
        else
        {
            enemyStatus = Status.PATROL;
        }
    }
}
