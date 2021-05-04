using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossDash : BossBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Move);
    }
}
