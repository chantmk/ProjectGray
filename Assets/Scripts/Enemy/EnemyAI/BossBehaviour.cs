using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    /**
     * This  class is base of other behaviour
     * - Determine when to chase
     * - Determine is bose should enrage
     * - Determine is boss should lastStand
     * - Determine is boss should attack/special attack
     */
    protected Boss boss;
    protected BossWeapon bossWeapon;
    protected BossStats bossStats;
    protected TalkManager talkManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        boss = (Boss)enemy;
        bossWeapon = (BossWeapon)enemyWeapon;
        bossStats = animator.gameObject.GetComponent<BossStats>();
        talkManager = boss.GetComponent<TalkManager>();

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

    public void BossStatusHandler(BossStatus bossStatus)
    {
        switch (bossStatus)
        {
            case BossStatus.Calm:
                Debug.Log(transform.name + " Calm");
                break;
            case BossStatus.Enrage:
                Debug.Log(transform.name + " Enrage");
                animator.SetTrigger("Enrage");
                break;
            case BossStatus.Hyper:
                Debug.Log(transform.name + " Hyper");
                animator.SetTrigger("Hyper");
                break;
            case BossStatus.LastStand:
                Debug.Log(transform.name + " LastStand");
                animator.SetTrigger("LastStand");
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    protected override void ListenToAttackSignal()
    {
        // Override attack to look for attack and special attack + prob
        float range = Vector2.Distance(player.position, transform.position);
        float randomRange = bossWeapon.SpecialAttackProbability + bossWeapon.AttackProbability;
        float random = Random.Range(0.0f, randomRange);

        //Debug.Log($"{random}, {bossWeapon.SpecialAttackProbability}");
        if (random <= bossWeapon.SpecialAttackProbability && range < bossWeapon.SpecialAttackRange && bossStats.Aggro == BossStatus.Enrage)
        {
            animator.SetTrigger("SpecialAttack");
        }
        else if (range < bossWeapon.AttackRange)
        {
            //Debug.Log("HI");
            animator.SetTrigger("Attack");
        }
    }
}
