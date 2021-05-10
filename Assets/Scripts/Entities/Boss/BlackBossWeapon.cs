using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlackBossWeapon : BossWeapon
{
    [SerializeField]
    private float enrageShootAngle = 60.0f;
    public override void EnrageAttack(int EnrageNumber)
    {
        base.EnrageAttack(EnrageNumber);
        var enrageAttackComponent = Instantiate(EnrageAttacks[EnrageNumber], transform.position, Quaternion.Euler(Vector3.zero));
        var rotation = Quaternion.AngleAxis(Random.Range(-enrageShootAngle, enrageShootAngle), Vector3.up);
        enrageAttackComponent.GetComponent<Projectile>().Shoot(rotation*enemyMovement.GetVectorToPlayer().normalized);
    }

    public override void HyperAttack(int HyperNumber)
    {
        base.HyperAttack(HyperNumber);
    }
}
