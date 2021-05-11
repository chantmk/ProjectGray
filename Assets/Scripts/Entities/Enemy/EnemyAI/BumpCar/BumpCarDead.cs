using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCarDead : BumpCarBehaviour
{
    private BumpCarMovement bumpcarMovement;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bumpcarMovement = (BumpCarMovement)enemyMovement;
        bumpcarMovement.Shoot();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        bumpcarMovement.Patrol();
    }
}
