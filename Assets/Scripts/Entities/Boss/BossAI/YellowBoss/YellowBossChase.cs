using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class YellowBossChase : BossChase
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
            animator.SetTrigger(AnimatorParams.Trap);
        }
    }
}
