using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : StateMachineBehaviour
{
    protected Cat cat;
    protected Animator animator;
    protected Rigidbody2D rigidbody;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        cat = animator.gameObject.GetComponent<Cat>();
        rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat.DecideFollow();
        updateMovingAnimation();
        updateSpeed();
        checkEmotion();
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

    private void updateMovingAnimation()
    {
        Vector2 direction = rigidbody.velocity.normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    private void updateSpeed()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
    }

    private void checkEmotion()
    {
        animator.SetInteger("Emotion", (int)cat.emoState);
    }
}
