using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehaviour
{
    /*public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        chase();
        ListenToAttackSignal();
        ListenToChaseSignal();
    }

    protected virtual void chase()
    {
        // This can be improved by use A* pathfinder
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.Speed * Time.deltaTime);
    }*/
}
