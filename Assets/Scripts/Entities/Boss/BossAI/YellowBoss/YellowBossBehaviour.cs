using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class YellowBossBehaviour : BossBehaviour
{
    protected override void ListenToAttackSignal()
    {
        // Override attack to look for attack and special attack + prob
        float randomRange = bossWeapon.HyperAttackRatio + bossWeapon.EnrageAttackRatio + bossWeapon.AttackRatio;
        float random = Random.Range(0.0f, randomRange);

        if (bossStats.Aggro == BossAggroEnum.Hyper && random <= bossWeapon.HyperAttackRatio)
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Normal))
            {
                animator.SetTrigger(AnimatorParams.HyperAttack);
            }
        }
        else if (bossStats.Aggro == BossAggroEnum.Enrage && random <= bossWeapon.EnrageAttackRatio + bossWeapon.HyperAttackRatio)
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Hyper))
            {
                animator.SetTrigger(AnimatorParams.EnrageAttack);
            }
        }
        else
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Enrage))
            {
                animator.SetTrigger(AnimatorParams.Attack);
            }
        }
    }
}
