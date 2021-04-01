using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    public GameObject[] SpecialAttacks = new GameObject[3];

    public void SpecialAttack()
    {
        Debug.Log("Special attack");
        SpecialAttack(0, Vector3.zero);
    }
    public virtual void SpecialAttack(int SpecialNumber, Vector3 spawnOffset)
    {
        //Vector2 attackPosition = Owner.transform.position + spawnOffset;
        // May check not exceed range of Owner
        //if (Math.abs(Vector2.Distance(attackPosition, Owner.transform.position)) > )
        var specialAttackComponent = Instantiate(SpecialAttacks[SpecialNumber], (Owner.transform.position+spawnOffset), Quaternion.Euler(Vector3.zero));
    }

}
