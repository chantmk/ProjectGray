using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        patrol();
        ListenToChaseSignal();
        updateMovingAnimation();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("IntoVisionRange");
    }
    private void patrol()
    {
        // This can be improved by use A* pathfinder
        transform.position = Vector2.MoveTowards(transform.position, enemyMovement.GetNextPatrolPosition(), enemyMovement.Speed * Time.deltaTime);
    }

    private void updateMovingAnimation()
    {
        var direction = (Vector3)enemyMovement.GetNextPatrolPosition() - transform.position;
        animator.SetFloat("Horizontal", direction.normalized.x);
    }
}
