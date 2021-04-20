using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossWeapon : BossWeapon
{

    [Header("Pattern parameter")]
    [SerializeField]
    private int EnrageBulletCount = 4;
    [SerializeField]
    private int HyperBulletCount = 8;

    public override void EnrageAttack(int EnrageNumber)
    {
        base.EnrageAttack(EnrageNumber);
        switch (bossStats.Aggro)
        {
            case (BossStatus.Enrage):
                for (int i=0; i<EnrageBulletCount; i++)
                {
                    Debug.Log("Enrage Enrage shoot");
                    var bullet = Instantiate(EnrageAttacks[0], transform.position, Quaternion.Euler(Vector3.zero));
                    bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                }
                break;
            case (BossStatus.Hyper):
                for (int i = 0; i < HyperBulletCount; i++)
                {
                    Debug.Log("Enrage Hyper shoot");
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
            var bullet = Instantiate(HyperAttacks[0], transform);
            bullet.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }
}
