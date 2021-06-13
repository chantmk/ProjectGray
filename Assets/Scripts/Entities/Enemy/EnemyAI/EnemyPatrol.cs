using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyPatrol : EnemyBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        enemyMovement.Patrol();
        if (enemyMovement.onlyPatrol)
        {
            ListenToAttackSignal();
        }
        ListenToChaseSignal();
        updateMovingAnimation();
    }
}
