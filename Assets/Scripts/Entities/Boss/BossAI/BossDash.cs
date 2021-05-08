using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossDash : BossBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bossMovement.Dash();
        bossWeapon.DashAttack();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToChaseSignal();
    }

    protected override void ListenToChaseSignal()
    {
        if (!bossMovement.IsDashing())
        {
            animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Move);
        }
    }
}
