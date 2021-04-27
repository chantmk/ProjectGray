using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon status")]
    public GameObject ProjectileComponent;
    public float AttackRange = 1.0f;
    public float AttackSpeed = 1.0f;
    public float AttackDamage = 1.0f;
    public float AttackMaxCooldown = 2.0f;
    public bool IsRange = false;
    
    private AttackHitbox attackHitbox;

    void Start()
    {
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
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
        Debug.Log($"Melee attack from {this.name}");
        HashSet<Collider2D> colliders = attackHitbox.HitColliders;
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider);
            // Below will deal damage to player if gameObject contain this collider has player class --> This may change to health class or something
            if (collider.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
            {
                Debug.Log("SHIT2");
                playerStats.TakeDamage(AttackDamage);
            }
        }
    }

    protected virtual void RangeAttack()
    {
        Debug.Log($"Range attack from {this.name}");
        // This method instantiate the projectile given in projectile component
        // Instant the position and then let the object do what it have to
        var projectile = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
