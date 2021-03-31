using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    protected Boss boss;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        boss = (Boss)enemy;
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
            animator.SetBool("IsEnrage", true);
            boss.IsEnrage = true;
        }
    }

    protected void ListenToLastStandSignal()
    {
        if (boss.GetPercentHealth() < boss.LastStandRatio && !animator.GetBool("IsLastStand"))
        {
            animator.SetBool("IsLastStand", true);
        }
    }

    protected override void ListenToAttackSignal()
    {
        // Override attack to look for attack and special attack + prob
        float range = Vector2.Distance(player.position, transform.position);
        if (range < boss.SpecialAttackRange && boss.IsEnrage)
        {
            Debug.Log("Boss Special Attack");
            animator.SetTrigger("SpecialAttack");
        }
        else if (range < boss.AttackRange)
        {
            Debug.Log("Boss Attack");
            animator.SetTrigger("Attack");
        }
    }
}
