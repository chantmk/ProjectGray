using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : StateMachineBehaviour
{
    protected Cat cat;
    protected Animator animator;
    protected Transform transform;
    protected Rigidbody2D rigidbody;

    private Vector3 nextPosition;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        cat = animator.gameObject.GetComponent<Cat>();
        transform = animator.gameObject.transform;
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveToNextPosition();
        updateMovingAnimation();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void moveToNextPosition()
    {
        nextPosition = cat.calculateNextPosition();
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, cat.MoveSpeed * Time.deltaTime);
        //rigidbody.velocity = (transform.position - cat.calculateNextPosition()) * cat.MoveSpeed;
    }

    private void updateMovingAnimation()
    {
        var direction = nextPosition - transform.position;
        animator.SetFloat("Horizontal", direction.normalized.x);
        animator.SetFloat("Vertical", direction.normalized.y);
    }
}
