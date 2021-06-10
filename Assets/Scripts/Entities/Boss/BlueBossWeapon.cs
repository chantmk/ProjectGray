using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlueBossWeapon : BossWeapon
{
    [Header("Pattern parameter")]
    [SerializeField]
    private int[] DashAttackCount = new int[3];
    [SerializeField]
    private int[] CalmAttackCount = new int[3];
    [SerializeField]
    private int[] CalmAttack2Count = new int[3];
    [SerializeField]
    private float CalmAttack2Range = 5.0f;
    [SerializeField]
    private int[] EnrageAttackCount = new int[3];
    [SerializeField]
    private int[] HyperAttackCount = new int[3];

    private int[] rangeAttackPattern = new int[] { 0, 1, 0, 1, 1 };
    private int rangeCount = 0;

    protected override void RangeAttack()
    {
        
        switch (rangeAttackPattern[rangeCount])
        {
            case 0:
                CalmAttack(0);
                break;
            case 1:
                CalmAttack2(0);
                break;
        }
        rangeCount += 1;
        if (rangeCount >= rangeAttackPattern.Length)
        {
            rangeCount = 0;
        }
    }
    public void CalmAttack(int EnrageNumber)
    {
        Debug.Log("Range Attack Blue0");
        float count = CalmAttackCount[(int)bossStats.Aggro];
        float degree = 360.0f / count;
        for(int i=0; i<count; i++)
        {
            var bullet = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(Quaternion.AngleAxis(degree*i, Vector3.forward) * new Vector2(1.0f, 0.0f));
        }
    }

    public void CalmAttack2(int EnrageNumber)
    {
        Debug.Log("Range Attack Blue1");
        float count = CalmAttack2Count[(int)bossStats.Aggro];
        float degree = 360.0f / count;
        for(int i=0; i<count; i++)
        {
            Vector2 position = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * CalmAttack2Range;
            var bullet = Instantiate(ProjectileComponent, centerPoint+position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(-position);
        }
    }

    public override void EnrageAttack(int EnrageNumber)
    {
        base.EnrageAttack(EnrageNumber);
        switch (bossStats.Aggro)
        {
            case (BossAggroEnum.Enrage):
                for (int i=0; i<EnrageAttackCount[1]; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                }
                break;
            case (BossAggroEnum.Hyper):
                for (int i = 0; i < EnrageAttackCount[2]; i++)
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
        base.HyperAttack(HyperNumber);
        Debug.Log("Hyper ");
        float count = HyperAttackCount[2];
        float degree = 360.0f / count;
        for (int i = 0; i < count; i++)
        {
            Vector2 position = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * HyperAttackRange*0.33f;
            var bullet = Instantiate(HyperAttacks[0], centerPoint + position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(-position);

            Vector2 position2 = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * HyperAttackRange*0.66f;
            var bullet2 = Instantiate(HyperAttacks[0], centerPoint + position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(-position);

            Vector2 position3 = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * HyperAttackRange;
            var bullet3 = Instantiate(HyperAttacks[0], centerPoint + position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(-position);
        }
    }

    public override void DashAttack()
    {
        base.DashAttack();
        float xToPlayer = enemyMovement.GetVectorToPlayer().x;
        float direction = xToPlayer / Mathf.Abs(xToPlayer);
        switch (bossStats.Aggro)
        {
            case (BossAggroEnum.Calm):
                break;
            case (BossAggroEnum.Enrage):
                for (int i = 0; i < DashAttackCount[1]; i++)
                {
                    var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    var bulletDirection = bullet.transform.localScale;
                    bullet.transform.localScale = new Vector3(direction * Mathf.Abs(bulletDirection.x), bulletDirection.y);
                }
                break;
            case (BossAggroEnum.Hyper):
                for (int i = 0; i < DashAttackCount[2]; i++)
                {
                    var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    var bulletDirection = bullet.transform.localScale;
                    bullet.transform.localScale = new Vector3(direction * Mathf.Abs(bulletDirection.x), bulletDirection.y);
                }
                break;
        }
    }
}
