using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlueBossWeapon : BossWeapon
{

    [Header("Pattern parameter")]
    [SerializeField]
    private int EnrageBulletCount = 4;
    [SerializeField]
    private int HyperBulletCount = 8;
    [SerializeField]
    private int DashAttackCount = 4;

    public override void EnrageAttack(int EnrageNumber)
    {
        base.EnrageAttack(EnrageNumber);
        switch (bossStats.Aggro)
        {
            case (BossAggroEnum.Enrage):
                for (int i=0; i<EnrageBulletCount; i++)
                {
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                }
                break;
            case (BossAggroEnum.Hyper):
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
        base.HyperAttack(HyperNumber);

        for (int i = 0; i < HyperBulletCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(HyperAttackRange / 2, HyperAttackRange), Random.Range(HyperAttackRange / 2, HyperAttackRange));
            var bullet = Instantiate(HyperAttacks[0], transform.position + position, Quaternion.Euler(Vector3.zero));
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
                for (int i = 0; i < DashAttackCount; i++)
                {
                    var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    var bulletDirection = bullet.transform.localScale;
                    bullet.transform.localScale = new Vector3(direction * Mathf.Abs(bulletDirection.x), bulletDirection.y);
                }
                break;
            case (BossAggroEnum.Hyper):
                for (int i = 0; i < DashAttackCount; i++)
                {
                    var bullet = Instantiate(DashAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    var bulletDirection = bullet.transform.localScale;
                    bullet.transform.localScale = new Vector3(direction * Mathf.Abs(bulletDirection.x), bulletDirection.y);
                }
                break;
        }
    }
}
