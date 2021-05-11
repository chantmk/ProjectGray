using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class YellowBossWeapon : BossWeapon
{
    [Header("Pattern parameter")]
    [SerializeField]
    private int EnrageBulletCount = 4;
    [SerializeField]
    private float EnrageSpawnRange = 5.0f;
    [SerializeField]
    private int HyperBulletCount = 8;
    [SerializeField]
    private float HyperSpawnRange = 5.0f;
    [SerializeField]
    private int DashAttackCount = 4;
    [SerializeField]
    private Vector2 centerPosition;
    [Header("Balloon trap parameter")]
    [SerializeField]
    private float TrapSpawnRange = 5.0f;
    [SerializeField]
    private float CalmTrapMaxCooldown = 1.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float CalmTrapRatio;
    [SerializeField]
    private float EnrageTrapMaxCooldown = 1.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float EnrageTrapRatio;
    [SerializeField]
    private float HyperTrapMaxCooldown = 1.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float HyperTrapRatio;
    [SerializeField]
    private GameObject trap;

    private float currentTrapCooldown = 0.0f;

    public override void Start()
    {
        base.Start();
        currentTrapCooldown = CalmTrapMaxCooldown;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        currentTrapCooldown -= Time.fixedDeltaTime;
    }

    private Vector3 RandomAroundCenter(float range)
    {
        return (Vector3)centerPosition + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(0.0f, range));
    }
    public override void EnrageAttack(int EnrageNumber)
    {
        // This method will Instantiate some roller coaster will be called by normal attack
        base.EnrageAttack(EnrageNumber);
        switch (bossStats.Aggro)
        {
            case (BossAggroEnum.Calm):
                for (int i = 0; i < 1; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[Random.Range(0, EnrageAttacks.Length)], RandomAroundCenter(EnrageSpawnRange), Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<BumpCarMovement>().Shoot();
                }
                break;
            case (BossAggroEnum.Enrage):
                for (int i = 0; i < EnrageBulletCount; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[Random.Range(0, EnrageAttacks.Length)], RandomAroundCenter(EnrageSpawnRange), Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<BumpCarMovement>().Shoot();
                }
                break;
            case (BossAggroEnum.Hyper):
                for (int i = 0; i < HyperBulletCount; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[Random.Range(0, EnrageAttacks.Length)], RandomAroundCenter(EnrageSpawnRange), Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<BumpCarMovement>().Shoot();
                }
                break;
            default:
                break;
        }
    }

    public override void HyperAttack(int HyperNumber)
    {
        // this method instantiate only one merry go round which will call by enrage attack
        base.HyperAttack(HyperNumber);
        for (int i = 0; i < HyperBulletCount; i++)
        {
            var bullet = Instantiate(HyperAttacks[0], centerPosition+(Vector2.up*2.0f), Quaternion.Euler(Vector3.zero));
            //bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public override void DashAttack()
    {
        // Dash and spawn balloon at it last place
        base.DashAttack();
        for (int i = 0; i < DashAttackCount; i++)
        {
            var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
            //bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public void SetTrap()
    {
        resetTrapCooldown();
        Instantiate(trap, transform.position, Quaternion.Euler(Vector3.zero));
    }

    public void RandomSpawnTrap()
    {
        resetTrapCooldown();
        Instantiate(trap, RandomAroundCenter(TrapSpawnRange), Quaternion.Euler(Vector3.zero));

    }

    public bool ShouldTrap()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (currentTrapCooldown <= 0.0f)
        {
            switch (bossStats.Aggro)
            {
                case BossAggroEnum.Calm:
                    return random < CalmTrapRatio;
                case BossAggroEnum.Enrage:
                    return random < EnrageTrapRatio;
                case BossAggroEnum.Hyper:
                    return random < HyperTrapRatio;
                case BossAggroEnum.LastStand:
                    return false;
                default:
                    return false;
            }
        }
        return false;
    }

    private void resetTrapCooldown()
    {
        switch (bossStats.Aggro)
        {
            case BossAggroEnum.Calm:
                currentTrapCooldown = CalmTrapMaxCooldown;
                break;
            case BossAggroEnum.Enrage:
                currentTrapCooldown = EnrageTrapMaxCooldown;
                break;
            case BossAggroEnum.Hyper:
                currentTrapCooldown = HyperTrapMaxCooldown;
                break;
            default:
                break;
        }
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPosition, 3.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(centerPosition, 0.5f);
    }
}
