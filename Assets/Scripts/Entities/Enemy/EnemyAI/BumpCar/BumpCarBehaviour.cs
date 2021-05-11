using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpCarBehaviour : EnemyBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        enemyMovement = animator.gameObject.GetComponent<BumpCarMovement>();
        enemyStats = animator.gameObject.GetComponent<EnemyStats>();
        enemyRigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        transform = animator.gameObject.transform;
    }

    protected override void ListenToAttackSignal()
    {
    }

    protected override void ListenToChaseSignal()
    {
    }
}
