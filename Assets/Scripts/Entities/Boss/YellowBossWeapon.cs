using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class YellowBossWeapon : BossWeapon
{
    [Header("Pattern parameter")]
    [SerializeField]
    private int[] EnrageAttackPatternCount = new int[3];
    [SerializeField]
    private int HyperBulletCount = 8;
    [SerializeField]
    private float HyperSpawnRange = 5.0f;
    [SerializeField]
    private int DashAttackCount = 4;
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
    private int[] enrageAttackPattern = new int[] { 0, 2, 0, 2, 1, 0, 1, 1, 2};
    private int enrageCount = 0;

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
        return (Vector3)centerPoint + (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(0.0f, range));
    }
    public override void EnrageAttack(int EnrageNumber)
    {
        if ((int)bossStats.Aggro >= enrageAttackPattern.Length)
        {
            return;
        }
        // This method will Instantiate some roller coaster will be called by normal attack
        base.EnrageAttack(EnrageNumber);
        switch(enrageAttackPattern[enrageCount])
        {
            case 0:
                EnragePattern1();
                break;
            case 1:
                EnragePattern2();
                break;
            case 2:
                EnragePattern1();
                EnragePattern2();
                break;
        }
        enrageCount = (enrageCount + 1)%enrageAttackPattern.Length;
    }

    private void EnragePattern1()
    {
        if ((int)bossStats.Aggro >= enrageAttackPattern.Length)
        {
            return;
        }
        int count = EnrageAttackPatternCount[(int)bossStats.Aggro];
        float delta = (TopRightCorner.x - BottomLeftCorner.x)/count;
        for (int i=0; i < count; i++)
        {
            var bulletTop = Instantiate(EnrageAttacks[i % 2], new Vector2(BottomLeftCorner.x + (i * delta), TopRightCorner.y), Quaternion.Euler(Vector3.zero));
            bulletTop.GetComponent<BumpCarMovement>().ShootToPlayer();
            var bulletBottom = Instantiate(EnrageAttacks[i % 2], new Vector2(BottomLeftCorner.x + (i * delta), BottomLeftCorner.y), Quaternion.Euler(Vector3.zero));
            bulletBottom.GetComponent<BumpCarMovement>().ShootToPlayer();
        }
    }

    private void EnragePattern2()
    {
        if ((int)bossStats.Aggro >= enrageAttackPattern.Length)
        {
            return;
        }
        int count = EnrageAttackPatternCount[(int)bossStats.Aggro];
        float delta = (TopRightCorner.y - BottomLeftCorner.y) / count;
        for (int i = 0; i < count; i++)
        {
            var bulletLeft = Instantiate(EnrageAttacks[i % 2], new Vector2(BottomLeftCorner.x , BottomLeftCorner.y + (i*delta) ), Quaternion.Euler(Vector3.zero));
            bulletLeft.GetComponent<BumpCarMovement>().Shoot();
            var bulletRight = Instantiate(EnrageAttacks[i % 2], new Vector2(TopRightCorner.x, BottomLeftCorner.y + (i*delta)), Quaternion.Euler(Vector3.zero));
            bulletRight.GetComponent<BumpCarMovement>().Shoot();
        }
    }


    public override void HyperAttack(int HyperNumber)
    {
        // this method instantiate only one merry go round which will call by enrage attack
        base.HyperAttack(HyperNumber);
        Debug.Log("Hyper ");
        for (int i = 0; i < HyperBulletCount; i++)
        {
            var bullet = Instantiate(HyperAttacks[0], centerPoint+(Vector2.up*2.0f), Quaternion.Euler(Vector3.zero));
            //bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public override void DashAttack()
    {
        // Dash and spawn balloon at it last place
        base.DashAttack();
        Debug.Log("Dash ");
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
        Gizmos.DrawWireSphere(centerPoint, 3.0f);
    }
}
