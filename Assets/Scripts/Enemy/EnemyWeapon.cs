using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon status")]
    public GameObject ProjectileComponent;
    public float AttackRange = 1f;
    public float AttackSpeed = 1f;
    public float AttackDamage = 1f;
    public bool IsRange = false;

    //private AttackHitbox attackHitbox;

    public void Start()
    {
        // Get the hitbox reference to access in melee attack later
        //attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
    }
    public virtual void Attack()
    {
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
        //HashSet<Collider2D> colliders = attackHitbox.HitColliders;
        //foreach (Collider2D collider in colliders)
        //{
        //    // Below will deal damage to player if gameObject contain this collider has player class --> This may change to health class or something
        //    //if (collider.TryGetComponent<Player>(out Player player))
        //    //{
        //    //    player.TakeDamage();
        //    //}
        //}
    }

    protected virtual void RangeAttack()
    {
        // This method instantiate the projectile given in projectile component
        // Instant the position and then let the object do what it have to
        var projectile = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
    }
}
