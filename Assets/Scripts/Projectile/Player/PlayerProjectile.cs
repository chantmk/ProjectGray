using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{

    protected override List<int> targetLayers
    {
        get { return _targetLayers; }
    }
    private List<int> _targetLayers;

    private void Awake()
    {
        _targetLayers = new List<int>() {LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Boss")};
    }

    protected override void Attack(GameObject target)
    {
        target.GetComponent<EnemyStats>().TakeDamage(damage);
    }
}

// public class Player : PlayereProjectile
// {
//     
// }