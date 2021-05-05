using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlackBossWeapon : BossWeapon
{
    public override void EnrageAttack(int EnrageNumber)
    {
        base.EnrageAttack(EnrageNumber);
        var enrageAttackComponent = Instantiate(EnrageAttacks[EnrageNumber], transform.position, Quaternion.Euler(Vector3.zero));
    }

    public override void HyperAttack(int HyperNumber)
    {
        base.HyperAttack(HyperNumber);
    }
}
