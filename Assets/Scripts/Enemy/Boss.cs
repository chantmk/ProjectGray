using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss life parameters")]
    [Range(0.0f, 1.0f)]
    public float EnrageRatio;
    [Range(0.0f, 1.0f)]
    public float LastStandRatio;
    [Header("Boss status parameters")]
    public bool IsEnrage = false;
    public bool IsLastStand = false;
    public bool IsImmortal = false;
    public bool IsMercy = false;
    public bool IsKill = false;

    protected override void Start()
    {
        base.Start();
    }

    public void ChooseMercy()
    {
        Debug.Log("Choose mercy");
        IsMercy = true;
    }
    public void ChooseKill()
    {
        Debug.Log("Choose Kill");
        IsKill = true;
    }
}
