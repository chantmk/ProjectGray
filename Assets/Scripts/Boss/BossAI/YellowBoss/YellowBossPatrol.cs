using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossPatrol : BossPatrol
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToRandomTrap();
    }
    private void ListenToRandomTrap()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (((YellowBossWeapon)bossWeapon).ShouldTrap())
        {
            animator.SetTrigger("SetTrap");
        }
    }
}
