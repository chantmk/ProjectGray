using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class YellowBossPatrol : YellowBossBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossStats.Status = StatusEnum.Immortal;
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToRandomTrap();
        bossMovement.Patrol();
        ListenToAttackSignal();
        updateMovingAnimation();
    }
    private void ListenToRandomTrap()
    {
        if (((YellowBossWeapon)bossWeapon).ShouldTrap())
        {
            animator.SetTrigger(AnimatorParams.Trap);
        }
    }

    private void updateMovingAnimation()
    {
        //var direction = bossMovement.GetVectorToPlayer();
        var direction = new Vector2(0.0f, -1.0f);
        animator.SetFloat(AnimatorParams.Horizontal, direction.normalized.x);
        animator.SetFloat(AnimatorParams.Vertical, direction.normalized.y);
    }
}
