using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : BossProjectile
{
    [SerializeField]
    private float pullStrength = 2.0f;
    [SerializeField]
    private float affectRange = 2.0f;

    private HashSet<GameObject> targets = new HashSet<GameObject>();
    

    public override void Update()
    {
        base.Update();
        if (targets.Count > 0)
        {
            pull();
        }
    }
    protected override void OnHitboxTriggerEnter(Collider2D other)
    {
        var targetGameobject = other.gameObject;
        if (targetLayers != null && targetLayers.Contains(targetGameobject.layer))
        {
            targets.Add(targetGameobject);
        }
    }

    protected override void OnHitboxTriggerExit(Collider2D other)
    {
        var targetGameobject = other.gameObject;
        if (targets.Contains(targetGameobject))
        {
            targets.Remove(targetGameobject);
        }
    }

    private void pull()
    {
        foreach (GameObject target in targets)
        {
            Vector2 direction = (transform.position - target.transform.position);
            float range = direction.magnitude;
            direction = direction.normalized;
            target.GetComponent<Rigidbody2D>().AddForce(direction * (((affectRange-Mathf.Min(0,(range-3f)))/affectRange) * pullStrength));
            Attack(target);
        }
    }

    protected override void Attack(GameObject target)
    {
        base.Attack(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, affectRange);
    }
}
