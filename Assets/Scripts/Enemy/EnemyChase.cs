using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        checkRange();
        chase();
    }

    private void checkRange()
    {
        if (Vector2.Distance(player.position, transform.position) < enemy.AttackRange)
        {
            animator.SetTrigger("IntoAttackRange");
        }
        else if (Vector2.Distance(player.position, transform.position) > enemy.VisionRange)
        {
            animator.SetBool("isInVisionRange", false);
        }
    }

    protected virtual void chase()
    {
        // This can be improved by use A* pathfinder
        if (Vector2.Distance(player.position, transform.position) > enemy.AttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.Speed * Time.deltaTime);
        }

    }
}
