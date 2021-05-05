using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerryGoRoundHorsePatrol : EnemyPatrol
{
    MerryGoRoundHorseMovement horseMovement;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        horseMovement = (MerryGoRoundHorseMovement)enemyMovement;
    }
    protected override void ListenToChaseSignal()
    {
        if(!horseMovement.IsIdle())
        {
            animator.SetTrigger("Chase");
        }
    }
}
