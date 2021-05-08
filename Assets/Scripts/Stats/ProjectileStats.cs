using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ProjectileStats : EnemyStats
{
    [SerializeField]
    private float consumeTime = 3.0f;

    private Animator animator;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        Status = StatusEnum.Dead;
        animator.SetTrigger("Dead");
        Destroy(gameObject, consumeTime);
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(status == Status.Dead && collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossStats>().TakeCrashDamage(damage);
        }
    }*/
}
