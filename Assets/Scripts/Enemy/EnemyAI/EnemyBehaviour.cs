using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : StateMachineBehaviour
{

    protected Animator animator;
    protected Transform transform;
    protected Rigidbody2D rigidbody2D;
    protected Enemy enemy;
    protected EnemyWeapon enemyWeapon;
    protected Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        enemy = animator.gameObject.GetComponent<Enemy>();
        enemyWeapon = animator.gameObject.GetComponent<EnemyWeapon>();
        transform = animator.gameObject.transform;
        player = enemy.player;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.FlipToPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }

    protected virtual void ListenToChaseSignal()
    {
        if (Vector2.Distance(player.position, transform.position) < enemy.VisionRange)
        {
            animator.SetBool("ShouldChase", true);
        }
        else
        {
            animator.SetBool("ShouldChase", false);
        }
    }
    protected virtual void ListenToAttackSignal()
    {
        if (enemy.GetVectorToPlayer().magnitude < enemyWeapon.AttackRange)
        {
            animator.SetTrigger("Attack");
        }
    }
}
