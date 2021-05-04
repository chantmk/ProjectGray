using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile: Projectile
{
    protected override List<int> targetLayers
    {
        get { return _targetLayers; }
    }
    private List<int> _targetLayers;

    private void Awake()
    {
        _targetLayers = new List<int>(){LayerMask.NameToLayer("Player")};
    }

    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(this.damage);
    }
}