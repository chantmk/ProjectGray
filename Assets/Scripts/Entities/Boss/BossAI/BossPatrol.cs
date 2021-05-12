using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossPatrol : BossBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        bossMovement.Patrol();
        ListenToAttackSignal();
        updateMovingAnimation();
    }

    private void updateMovingAnimation()
    {
        var direction = bossMovement.GetVectorToPlayer();
        animator.SetFloat(AnimatorParams.Horizontal, direction.normalized.x);
        animator.SetFloat(AnimatorParams.Vertical, direction.normalized.y);
    }
}
