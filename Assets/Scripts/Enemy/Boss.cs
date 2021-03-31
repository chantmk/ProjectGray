using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss health parameters")]
    [Range(0.0f, 1.0f)]
    public float EnrageRatio;
    [Range(0.0f, 1.0f)]
    public float LastStandRatio;
    [Header("Boss attack parameters")]
    public bool IsEnrage = false;
    public float SpecialAttackRange;
    [Range(0.0f, 1.0f)]
    public float AttackProbability;
    [Range(0.0f, 1.0f)]
    public float SpecialProbability;
    [Tooltip("Boss special attack component")]
    public float placeholder;
    protected override void Start()
    {
        base.Start();
    }
}
