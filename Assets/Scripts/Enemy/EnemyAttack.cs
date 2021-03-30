using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attack();
    }
    public void Attack()
    {
        if (enemy.IsRange)
        {
            RangeAttack();
        }
        else
        {
            MeleeAttack();
        }
    }

    private void RangeAttack()
    {
        // This method will instant the bullet prefab and guied direction of it
    }
    
    private void MeleeAttack()
    {
        // This method should check if attack hitbox collide with player
        // Then call deal damage to player
    }
}
