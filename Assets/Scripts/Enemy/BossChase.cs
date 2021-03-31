using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : BossBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToAttackSignal();
        chase();
    }

    private void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.Speed * Time.deltaTime);
    }
}
