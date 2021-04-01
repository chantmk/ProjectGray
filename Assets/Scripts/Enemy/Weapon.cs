using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Owner;
    [Header("Projectile object")]
    public GameObject ProjectileComponent;
    public GameObject HitboxComponent;
    [Header("Weapon status")]
    public float AttackDamage;
    public LayerMask Target;
    public bool IsRange;

    public virtual void Attack()
    {
        Debug.Log($"Attacks from {Owner.name}");
        if (IsRange)
        {
            RangeAttack();
        }
        else
        {
            MeleeAttack();
        }
    }

    protected virtual void MeleeAttack()
    {
        HashSet<Collider2D> colliders = HitboxComponent.GetComponent<AttackHitbox>().HitColliders;
        foreach (Collider2D collider in colliders)
        {
            // collider.TryGetComponent(Player);
            // Deal damage
        }
    }

    protected virtual void RangeAttack()
    {
        var projectile = Instantiate(ProjectileComponent, Owner.transform.position, Quaternion.Euler(Vector3.zero));
    }
}
