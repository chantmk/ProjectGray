using System.Collections.Generic;
using UnityEngine;

public class PuddleSplash : Projectile
{

    protected override List<int> targetLayers
    {
        get { return _targetLayers; }
    }

    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    private List<int> _targetLayers;

    private void Awake()
    {
        _targetLayers = new List<int>() {LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Boss")};
    }

}