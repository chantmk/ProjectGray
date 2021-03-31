using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attack();
    }

    // Dont need this bcuz After attack we let animator to change state back to chase automatically
    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    ListenToChaseSignal();
    //}

    public void Attack()
    {
        if (boss.IsRange)
        {
            RangeAttack();
        }
        else
        {
            MeleeAttack();
        }
    }

    // This method should provided special attack position
    public void SpecialAttack()
    {
        // This will call attack() of SpecialAttack component
        // The SpecialAttack component should have collision detector and return damage dealt
        // This mean Boss class must contain SpecialAttack component
    }

    private void RangeAttack()
    {
        // This method is work somewhat like Special attack
    }
    private void MeleeAttack()
    {
        // This method should get bounding box of the attack hitbox of boss itself and check whether collide or not
        // Then deal damage
    }

}
