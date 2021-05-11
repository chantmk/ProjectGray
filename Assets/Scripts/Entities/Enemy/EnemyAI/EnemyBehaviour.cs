using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyBehaviour : StateMachineBehaviour
{

    protected Animator animator;
    protected Transform transform;
    protected Rigidbody2D enemyRigidbody;
    protected EnemyMovement enemyMovement;
    protected EnemyWeapon enemyWeapon;
    //protected Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        enemyRigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        enemyMovement = animator.gameObject.GetComponent<EnemyMovement>();
        enemyWeapon = animator.gameObject.GetComponent<EnemyWeapon>();
        transform = animator.gameObject.transform;
        //player = enemyMovement.player.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyMovement.ManualFlip)
        {
            enemyMovement.Flip();
        }
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
        if (enemyMovement.ShouldChase())
        {
            animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Move);
        }
        else
        {
            animator.SetInteger(AnimatorParams.Movement, (int)MovementEnum.Idle);
        }
    }
    protected virtual void ListenToAttackSignal()
    {
        //TODO: This should change to cone view interaction 
        if (enemyWeapon.IsReady(enemyMovement.GetVectorToPlayer()))
        {
            animator.SetTrigger(AnimatorParams.Attack);
        }
    }

    protected void updateMovingAnimation()
    {
        var direction = enemyMovement.GetHeadingDirection();
        animator.SetFloat(AnimatorParams.Horizontal, direction.normalized.x);
    }
}
