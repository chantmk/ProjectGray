﻿using System.Collections;
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
    private float CalmAttack2Range = 20.0f;
    [SerializeField]
    private int[] EnrageAttackCount = new int[3];
    [SerializeField]
    private float EnrageAttackPatternRange = 10.0f;
    [SerializeField]
    private int[] EnrageAttackCount2 = new int[3];
    [SerializeField]
    private float EnrageAttackPattern2Range = 10.0f;
    [SerializeField]
    private int[] HyperAttackCount = new int[3];
    [SerializeField]
    private float HyperAttackPatternRange = 20.0f;

    private int[] rangeAttackPattern = new int[] { 0, 1, 0, 1, 1 };
    private int rangeCount = 0;

    private int[] enrageAttackPattern = new int[] { 0, 1, 0, 1, 2, 0, 1, 0, 1, 0 };
    private int enrageCount = 0;
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
        rangeCount = (rangeCount + 1) % rangeAttackPattern.Length;
    }
    public void CalmAttack(int EnrageNumber)
    {
        if((int)bossStats.Aggro >= CalmAttackCount.Length)
        {
            return;
        }
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
        if ((int)bossStats.Aggro >= CalmAttack2Count.Length)
        {
            return;
        }
        float count = CalmAttack2Count[(int)bossStats.Aggro];
        float degree = 360.0f / count;
        for(int i=0; i<count; i++)
        {
            Vector2 position = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * CalmAttack2Range;
            var bullet = Instantiate(ProjectileComponent, centerPoint+position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(-position.normalized);
        }
    }

    public override void EnrageAttack(int EnrageNumber)
    {
        if ((int)bossStats.Aggro >= EnrageAttackCount.Length)
        {
            return;
        }
        base.EnrageAttack(EnrageNumber);
        switch (enrageAttackPattern[enrageCount])
        {
            case 0:
                EnrageAttackPattern(EnrageNumber);
                break;
            case 1:
                EnrageAttackPattern2(EnrageNumber);
                break;
            case 2:
                EnrageAttackPattern3(EnrageNumber);
                break;
        }
        enrageCount = (enrageCount + 1)%enrageAttackPattern.Length;
    }

    public void EnrageAttackPattern(int EnrageNumber)
    {
        if ((int)bossStats.Aggro >= EnrageAttackCount.Length)
        {
            return;
        }
        int count = EnrageAttackCount[(int)bossStats.Aggro];
        float delta = EnrageAttackRange / count;
        var bullet = Instantiate(EnrageAttacks[0], centerPoint, Quaternion.Euler(Vector3.zero));
        bullet.GetComponent<Projectile>().Shoot(Vector2.right);
        for (int i = 1; i < count; i++)
        {
            var bullet1 = Instantiate(EnrageAttacks[0], centerPoint + (Vector2.left * delta * i), Quaternion.Euler(Vector3.zero));
            var bullet2 = Instantiate(EnrageAttacks[0], centerPoint + (Vector2.right * delta * i), Quaternion.Euler(Vector3.zero));
            bullet1.GetComponent<Projectile>().Shoot(Vector2.up * ((i % 2 * -2) + 1));
            bullet2.GetComponent<Projectile>().Shoot(Vector2.down * ((i % 2 * -2) + 1));
        }
    }

    public void EnrageAttackPattern2(int EnrageNumber)
    {
        if ((int)bossStats.Aggro >= EnrageAttackCount2.Length)
        {
            return;
        }
        int count = EnrageAttackCount2[(int)bossStats.Aggro];
        float delta = EnrageAttackRange / count;
        var bullet = Instantiate(EnrageAttacks[0], centerPoint, Quaternion.Euler(Vector3.zero));
        bullet.GetComponent<Projectile>().Shoot(Vector2.right);
        for (int i = 1; i < count; i++)
        {
            var bullet1 = Instantiate(EnrageAttacks[0], centerPoint + (Vector2.up * delta * i), Quaternion.Euler(Vector3.zero));
            var bullet2 = Instantiate(EnrageAttacks[0], centerPoint + (Vector2.down * delta * i), Quaternion.Euler(Vector3.zero));
            bullet1.GetComponent<Projectile>().Shoot(Vector2.right * ((i % 2 * -2) + 1));
            bullet2.GetComponent<Projectile>().Shoot(Vector2.left * ((i % 2 * -2) + 1));
        }
    }

    public void EnrageAttackPattern3(int EnrageNumber)
    {
        if ((int)bossStats.Aggro >= EnrageAttackCount2.Length)
        {
            return;
        }
        int count = EnrageAttackCount2[(int)bossStats.Aggro];
        float degree = 360.0f / count;
        for (int i = 0; i < count; i++)
        {
            Vector2 position = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * EnrageAttackPattern2Range/2.0f;
            var bullet = Instantiate(EnrageAttacks[0], centerPoint + position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<Projectile>().Shoot(Quaternion.AngleAxis(90, Vector3.forward)*position.normalized);
        }
    }

    public override void HyperAttack(int HyperNumber)
    {
        if ((int)bossStats.Aggro >= HyperAttackCount.Length)
        {
            return;
        }
        base.HyperAttack(HyperNumber);
        float count = HyperAttackCount[2];
        float degree = 360.0f / count;
        for (int i = 0; i < count; i++)
        {
            Vector2 position = Quaternion.AngleAxis(degree * i, Vector3.forward) * Vector3.right * HyperAttackPatternRange*0.33f;
            var bullet = Instantiate(HyperAttacks[0], centerPoint + position, Quaternion.Euler(Vector3.zero));
            //bullet.GetComponent<Projectile>().Shoot(-position);

            Vector2 position2 = Quaternion.AngleAxis(degree * i+30, Vector3.forward) * Vector3.right * HyperAttackPatternRange * 0.66f;
            var bullet2 = Instantiate(HyperAttacks[1], centerPoint + position2, Quaternion.Euler(Vector3.zero));
            //bullet2.GetComponent<Projectile>().Shoot(-position);

            Vector2 position3 = Quaternion.AngleAxis(degree * i+60, Vector3.forward) * Vector3.right * HyperAttackPatternRange;
            var bullet3 = Instantiate(HyperAttacks[2], centerPoint + position3, Quaternion.Euler(Vector3.zero));
            //bullet3.GetComponent<Projectile>().Shoot(-position);
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

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, CalmAttack2Range);
        Gizmos.DrawWireSphere(transform.position, HyperAttackPatternRange);
        Gizmos.DrawWireSphere(transform.position, EnrageAttackPattern2Range);
        Gizmos.DrawWireSphere(transform.position, EnrageAttackPatternRange);
        Gizmos.DrawWireSphere(transform.position, EnrageAttackPatternRange);
    }
}
