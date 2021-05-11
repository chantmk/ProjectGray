using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCarPatrol : BumpCarBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        enemyMovement.Patrol();
    }
}
