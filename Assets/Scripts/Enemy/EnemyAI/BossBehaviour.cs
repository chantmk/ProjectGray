using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    protected Boss boss;
    protected BossWeapon bossWeapon;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        boss = (Boss)enemy;
        bossWeapon = (BossWeapon)enemyWeapon;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        ListenToEnrageSignal();
        ListenToLastStandSignal();
    }

    protected void ListenToEnrageSignal()
    {
        if (boss.GetPercentHealth() < boss.EnrageRatio && !boss.IsEnrage)
        {
            animator.SetTrigger("Enrage");
            boss.IsEnrage = true;
        }
    }

    protected void ListenToLastStandSignal()
    {
        if (boss.GetPercentHealth() < boss.LastStandRatio && !animator.GetBool("IsLastStand"))
        {
            animator.SetTrigger("LastStand");
            boss.IsLastStand = true;
        }
    }

    protected override void ListenToAttackSignal()
    {
        // Override attack to look for attack and special attack + prob
        float range = Vector2.Distance(player.position, transform.position);
        float randomRange = bossWeapon.SpecialAttackProbability + bossWeapon.AttackProbability;
        float random = Random.Range(0.0f, randomRange);

        //Debug.Log($"{random}, {bossWeapon.SpecialAttackProbability}");
        if (random <= bossWeapon.SpecialAttackProbability && range < bossWeapon.SpecialAttackRange && boss.IsEnrage)
        {
            animator.SetTrigger("SpecialAttack");
        }
        else if (range < bossWeapon.AttackRange)
        {
            //Debug.Log("HI");
            animator.SetTrigger("Attack");
        }
    }
}
