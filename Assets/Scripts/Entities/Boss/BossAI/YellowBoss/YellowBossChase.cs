using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class YellowBossChase : YellowBossBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bossMovement.Chase();
        ListenToAttackSignal();
        ListenToDashSignal();
        updateMovingAnimation();
        if (bossStats.Aggro == BossAggroEnum.Hyper)
        {
            bossStats.Status = StatusEnum.Mortal;
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToRandomTrap();
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
        var direction = enemyRigidbody.velocity.normalized;
        animator.SetFloat(AnimatorParams.Horizontal, direction.x);
        animator.SetFloat(AnimatorParams.Vertical, direction.y);
    }

    private void ListenToDashSignal()
    {
        if (enemyMovement.IsReadyToDash())
        {
            animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Roll);
        }
    }
}
