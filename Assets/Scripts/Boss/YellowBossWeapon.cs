using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossWeapon : BossWeapon
{
    [Header("Pattern parameter")]
    [SerializeField]
    private int EnrageBulletCount = 4;
    [SerializeField]
    private int HyperBulletCount = 8;
    [SerializeField]
    private int DashAttackCount = 4;
    [Header("Balloon trap parameter")]
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

    public override void EnrageAttack(int EnrageNumber)
    {
        // This method will Instantiate some roller coaster will be called by normal attack
        base.EnrageAttack(EnrageNumber);
        switch (bossStats.Aggro)
        {
            case (BossStatus.Calm):
                for (int i = 0; i < 1; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                }
                break;
            case (BossStatus.Enrage):
                for (int i = 0; i < EnrageBulletCount; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                }
                break;
            case (BossStatus.Hyper):
                for (int i = 0; i < HyperBulletCount; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
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
            var bullet = Instantiate(HyperAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public override void DashAttack()
    {
        // Dash and spawn balloon at it last place
        base.DashAttack();
        for (int i = 0; i < DashAttackCount; i++)
        {
            var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
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
        Vector3 position = new Vector3(Random.Range(0.0f, 1.0f) * EnrageAttackRange, Random.Range(0.0f, 1.0f) * EnrageAttackRange);
        Instantiate(trap, position, Quaternion.Euler(Vector3.zero));

    }

    public bool ShouldTrap()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (currentTrapCooldown <= 0.0f)
        {
            switch (bossStats.Aggro)
            {
                case BossStatus.Calm:
                    return random < CalmTrapRatio;
                case BossStatus.Enrage:
                    return random < EnrageTrapRatio;
                case BossStatus.Hyper:
                    return random < HyperTrapRatio;
                case BossStatus.LastStand:
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
            case BossStatus.Calm:
                currentTrapCooldown = CalmTrapMaxCooldown;
                break;
            case BossStatus.Enrage:
                currentTrapCooldown = EnrageTrapMaxCooldown;
                break;
            case BossStatus.Hyper:
                currentTrapCooldown = HyperTrapMaxCooldown;
                break;
            default:
                break;
        }
    }
}
