using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : EnemyBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        knocked();
    }

    private void knocked()
    {
        transform.position += enemyMovement.GetVectorToPlayer() * Time.deltaTime;
    }
}
