using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Normal,
    Enrage,
    Hyper
}

public class BossWeapon : EnemyWeapon
{
    /**
     * This class tell weapon stat and what to create
     */
    [Header("Boss attack parameters")]
    [Header("Dash attack parameters")]
    public float DashAttackEffectRange;
    public float DashAttackMaxCooldown;
    [Header("Enrage attack parameters")]
    public float EnrageAttackRange;
    [Range(0.0f, 1.0f)]
    public float EnrageAttackRatio;
    public float EnrageAttackMaxCooldown;
    [Tooltip("Boss enrage attack component")]
    public GameObject[] EnrageAttacks = new GameObject[1];

    [Header("Hyper attack parameters")]
    public float HyperAttackRange;
    [Range(0.0f, 1.0f)]
    public float HyperAttackRatio;
    public float HyperAttackMaxCooldown;
    [Tooltip("Boss hyper attack component")]
    public GameObject[] HyperAttacks = new GameObject[1];

    protected BossStats bossStats;

    private float EnrageAttackCooldown;
    private float HyperAttackCooldown;
    private float DashAttackCooldown;

    public override void Start()
    {
        base.Start();
        EnrageAttackCooldown = EnrageAttackMaxCooldown;
        HyperAttackCooldown = HyperAttackMaxCooldown;
        DashAttackCooldown = DashAttackMaxCooldown;
    }

    public override void GetRelateComponent()
    {
        bossStats = GetComponent<BossStats>();
        attackDamage = bossStats.damage;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (EnrageAttackCooldown > 0.0f)
        {
            EnrageAttackCooldown -= Time.fixedDeltaTime;
        }

        if (HyperAttackCooldown > 0.0f)
        {
            HyperAttackCooldown -= Time.fixedDeltaTime;
        }
    }

    public virtual bool IsReady(Vector3 vectorToPlayer, AttackType attackType)
    {
        switch (attackType)
        {
            case (AttackType.Normal):
                return IsReady(vectorToPlayer);
            case (AttackType.Enrage):
                if (EnrageAttackCooldown <= 0.0f && vectorToPlayer.magnitude < EnrageAttackRange)
                {
                    return true;
                }
                return false;
            case (AttackType.Hyper):
                if (HyperAttackCooldown <= 0.0f && vectorToPlayer.magnitude < HyperAttackRange)
                {
                    return true;
                }
                return false;
            default:
                return false;
        }
    }

    public virtual void EnrageAttack(int EnrageNumber)
    {
        //TODO override Enrage pattern here should check which boss state
        EnrageAttackCooldown = EnrageAttackMaxCooldown;
        //var enrageAttackComponent = Instantiate(EnrageAttacks[EnrageNumber], transform.position, Quaternion.Euler(Vector3.zero));
    }

    public virtual void HyperAttack(int HyperNumber)
    {
        // TODO override Hyper pattern here should check which boss state
        HyperAttackCooldown = HyperAttackMaxCooldown;
        //var hyperAttackComponent = Instantiate(HyperAttacks[HyperNumber], transform.position, Quaternion.Euler(Vector3.zero));
    }

    public virtual void DashAttack()
    {
        // TODO override Dash attack pattern here should check which boss state
        Debug.Log("DashAttack " + DashAttackEffectRange);
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnrageAttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HyperAttackRange);
    }
}
