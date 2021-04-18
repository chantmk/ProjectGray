using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : BossBehaviour
{
    /**
     * This class work on chasing the player this will always listen if it have to attack
     */

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToAttackSignal();
        chase();
    }

    private void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemy.player.position, enemy.Speed * Time.deltaTime);
    }
}
