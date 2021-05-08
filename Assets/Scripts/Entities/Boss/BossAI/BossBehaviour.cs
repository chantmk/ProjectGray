using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossBehaviour : EnemyBehaviour
{
    /**
     * This  class is base of other behaviour
     * - Determine when to chase
     * - Determine is bose should enrage
     * - Determine is boss should lastStand
     * - Determine is boss should attack/special attack
     */
    protected BossMovement bossMovement;
    protected BossWeapon bossWeapon;
    protected BossStats bossStats;
    protected TalkManager talkManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        bossMovement = (BossMovement)enemyMovement;
        bossWeapon = (BossWeapon)enemyWeapon;
        bossStats = animator.gameObject.GetComponent<BossStats>();
        talkManager = bossMovement.GetComponent<TalkManager>();

        EventPublisher.StatusChange += BossStatusHandler;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        EventPublisher.StatusChange -= BossStatusHandler;
    }

    public void BossStatusHandler(BossAggroEnum bossAggro)
    {
        bossStats.Status = StatusEnum.Mortal;
        //Debug.Log(transform.name + ": " + bossAggro.ToString());
        animator.SetInteger(AnimatorParams.Aggro, (int)bossAggro);
        bossMovement.SetSpotCap(bossAggro);
    }

    protected override void ListenToAttackSignal()
    {
        // Override attack to look for attack and special attack + prob
        float randomRange = bossWeapon.HyperAttackRatio + bossWeapon.EnrageAttackRatio + bossWeapon.AttackRatio;
        float random = Random.Range(0.0f, randomRange);
        if (bossStats.Aggro == BossAggroEnum.Hyper && random <= bossWeapon.HyperAttackRatio)
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Hyper))
            {
                animator.SetTrigger(AnimatorParams.HyperAttack);
            }
        }
        else if (bossStats.Aggro == BossAggroEnum.Enrage && random <= bossWeapon.EnrageAttackRatio + bossWeapon.HyperAttackRatio)
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Enrage))
            {
                animator.SetTrigger(AnimatorParams.EnrageAttack);
            }
        }
        else
        {
            if (bossWeapon.IsReady(bossMovement.GetVectorToPlayer(), BossAttackEnum.Normal))
            {
                animator.SetTrigger(AnimatorParams.Attack);
            }
        }
    }
}
