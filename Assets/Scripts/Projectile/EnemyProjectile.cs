using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyProjectile: Projectile
{
    protected Animator animator;
    protected override List<int> targetLayers
    {
        get { return _targetLayers; }
    }
    private List<int> _targetLayers;

    private void Awake()
    {
        _targetLayers = new List<int>(){LayerMask.NameToLayer("Player")};
    }

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Execute()
    {
        animator.SetTrigger(AnimatorParams.Execute);
    }

    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(this.damage);
    }
}