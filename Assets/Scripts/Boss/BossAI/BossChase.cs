﻿using System.Collections;
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
        ListenToDashSignal();
        chase();
        updateMovingAnimation();
    }

    private void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemyMovement.Speed * Time.deltaTime);
    }

    private void updateMovingAnimation()
    {
        var direction = player.position - transform.position;
        animator.SetFloat("Horizontal", direction.normalized.x);
        animator.SetFloat("Vertical", direction.normalized.y);
    }

    private void ListenToDashSignal()
    {
        if (enemyMovement.IsReadyToDash())
        {
            animator.SetTrigger("Dash");
        }
    }
}