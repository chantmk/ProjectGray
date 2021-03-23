using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        patrol();
        if (Vector2.Distance(player.position, transform.position) < enemy.VisionRange)
        {
            //animator.SetTrigger("IntoVisionRange");
            animator.SetBool("isInVisionRange", true);
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("IntoVisionRange");
    }
    private void patrol()
    {
        // This can be improved by use A* pathfinder
        transform.position = Vector2.MoveTowards(transform.position, enemy.GetNextPatrolPosition(), enemy.Speed * Time.deltaTime);
    }
}
