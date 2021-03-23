using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss parameters")]
    public float SpecialAttackRange;

    override protected void Start()
    {
        base.Start();
    }
}
