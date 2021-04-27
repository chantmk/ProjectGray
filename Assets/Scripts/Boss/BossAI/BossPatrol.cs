using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : BossBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bossStats.status = Status.Immortal;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        patrol();
        updateMovingAnimation();
    }

    private void patrol()
    {
        // This can be improved by use A* pathfinder
        transform.position = Vector2.MoveTowards(transform.position, bossMovement.GetNextPatrolPosition(), bossMovement.Speed * Time.deltaTime);
    }

    private void updateMovingAnimation()
    {
        var direction = player.position - transform.position;
        animator.SetFloat("Horizontal", direction.normalized.x);
        animator.SetFloat("Vertical", direction.normalized.y);
    }
}
