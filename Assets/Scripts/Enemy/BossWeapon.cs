using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : EnemyWeapon
{
    [Header("Boss attack parameters")]
    public float SpecialAttackRange;
    [Range(0.0f, 1.0f)]
    public float AttackProbability;
    [Range(0.0f, 1.0f)]
    public float SpecialAttackProbability;
    [Tooltip("Boss special attack component")]
    public GameObject[] SpecialAttacks = new GameObject[3];

    public float SpawnOffset = 0.5f;
    public Transform player;

    public void SpecialAttack()
    {
        Debug.Log($"Special attack from {this.name}");
        SpecialAttack(0, Vector3.zero);
    }
    
    public virtual void SpecialAttack(int SpecialNumber, Vector3 spawnOffset)
    {
        //Vector2 attackPosition = Owner.transform.position + spawnOffset;
        // May check not exceed range of Owner
        //if (Math.abs(Vector2.Distance(attackPosition, Owner.transform.position)) > )
        var specialAttackComponent = Instantiate(SpecialAttacks[SpecialNumber], (Vector3.Lerp(transform.position, player.position, SpawnOffset)), Quaternion.Euler(Vector3.zero));
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, SpecialAttackRange);
    }
}
