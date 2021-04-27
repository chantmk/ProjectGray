using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWater : Projectile
{

    public override void Start()
    {
        duration = MaxDuration;
        mRigidbody = GetComponent<Rigidbody2D>();
        attackHitbox = GetComponent<AttackHitbox>();
    }
    protected override void Attack(GameObject target)
    {
        Debug.Log("Splashed!");
    }

    public void Clear()
    {
        duration = 0.0f;
    }
}
