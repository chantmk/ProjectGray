using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BossWeapon : EnemyWeapon
{
    /**
     * This class tell weapon stat and what to create
     */
    [SerializeField]
    protected Vector2 centerPoint;

    [Header("Dash attack parameters")]
    public float DashAttackEffectRange;
    public float DashAttackMaxCooldown;
    public GameObject[] DashAttacks = new GameObject[1];
    
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
    public AudioClip enrageSound;
    public float enrageVolume = 1f;
    public AudioClip hyperSound;
    public float hyperVolume = 1f;
    public AudioClip dashSound;
    public float dashVolume = 1f;


    public override void Start()
    {
        base.Start();
        EnrageAttackCooldown = EnrageAttackMaxCooldown;
        HyperAttackCooldown = HyperAttackMaxCooldown;
        DashAttackCooldown = DashAttackMaxCooldown;
        bossStats = GetComponent<BossStats>();
        audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (EnrageAttackCooldown > GrayConstants.MINIMUM_TIME)
        {
            EnrageAttackCooldown -= Time.fixedDeltaTime;
        }

        if (HyperAttackCooldown > GrayConstants.MINIMUM_TIME)
        {
            HyperAttackCooldown -= Time.fixedDeltaTime;
        }

        if (DashAttackCooldown > GrayConstants.MINIMUM_TIME)
        {
            DashAttackCooldown -= Time.fixedDeltaTime;
        }
    }

    public virtual bool IsReady(Vector3 vectorToPlayer, BossAttackEnum attackType)
    {
        switch (attackType)
        {
            case (BossAttackEnum.Normal):
                return IsReady(vectorToPlayer);
            case (BossAttackEnum.Enrage):
                if (EnrageAttackCooldown <= GrayConstants.MINIMUM_TIME && vectorToPlayer.magnitude < EnrageAttackRange)
                {
                    return true;
                }
                return false;
            case (BossAttackEnum.Hyper):
                if (HyperAttackCooldown <= GrayConstants.MINIMUM_TIME && vectorToPlayer.magnitude < HyperAttackRange)
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
        audioSrc.PlayOneShot(enrageSound, enrageVolume);
        EnrageAttackCooldown = EnrageAttackMaxCooldown;
    }

    public virtual void HyperAttack(int HyperNumber)
    {
        // TODO override Hyper pattern here should check which boss state
        audioSrc.PlayOneShot(hyperSound, hyperVolume);
        Debug.Log("Hyper ");
        HyperAttackCooldown = HyperAttackMaxCooldown;
    }

    public virtual void DashAttack()
    {
        // TODO override Dash attack pattern here should check which boss state
        audioSrc.PlayOneShot(dashSound, dashVolume);
        DashAttackCooldown = DashAttackMaxCooldown;
        Debug.Log("DashAttack: " + DashAttackEffectRange);
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnrageAttackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HyperAttackRange);

        Gizmos.DrawSphere(centerPoint, 2);
    }
}
