using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : Projectile
{
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }
}
