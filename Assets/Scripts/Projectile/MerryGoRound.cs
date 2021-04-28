using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerryGoRound : Projectile
{
    [SerializeField]
    private Vector3 horseRadius;
    /**
     * Instantiate horses and set position that horse need to move
     * If player collide with this do damage
    */

    public override void Start()
    {
        base.Start();
        spawnHorse();
    }

    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
    }

    private void spawnHorse()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + horseRadius, 0.3f);
    }
}
