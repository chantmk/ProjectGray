using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseDead : EnemyBehaviour
{
    MerryGoRoundHorseMovement horseMovement;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        horseMovement = ((MerryGoRoundHorseMovement)enemyMovement);
        horseMovement.Shoot();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        horseMovement.Fly();
    }
}
