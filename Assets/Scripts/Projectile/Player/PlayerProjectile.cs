using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public abstract class PlayerProjectile : Projectile
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
}