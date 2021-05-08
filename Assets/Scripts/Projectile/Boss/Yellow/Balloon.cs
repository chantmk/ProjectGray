using System.Collections;
using UnityEngine;

public class Balloon : BossProjectile
{

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
        duration = 0;
        animator.SetTrigger("Pop");
    }
}