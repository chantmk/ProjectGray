using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDead : EnemyBehaviour
{
    private Vector3 direction;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        direction = transform.position - player.position;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        knocked();
    }
    private void knocked()
    {
        transform.position += direction * Time.deltaTime;
    }
}
