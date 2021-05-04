using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossPatrol : BossBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        patrol();
        ListenToAttackSignal();
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
        animator.SetFloat(AnimatorParams.Horizontal, direction.normalized.x);
        animator.SetFloat(AnimatorParams.Vertical, direction.normalized.y);
    }
}
