using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWater : BossProjectile
{
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }
}
