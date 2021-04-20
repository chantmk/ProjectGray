using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon status")]
    public float AttackRange = 1.0f;
    //public float AttackSpeed = 1.0f;
    [Range(0.0f, 1.0f)]
    public float AttackRatio;
    public float AttackMaxCooldown = 2.0f;
    public bool IsRange = false;
    public GameObject ProjectileComponent;

    protected float attackDamage;
    private AttackHitbox attackHitbox;
    private float attackCooldown;
    
    public virtual void Start()
    {
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
        attackCooldown = AttackMaxCooldown;
        GetRelateComponent();
    }

    public virtual void GetRelateComponent()
    {
        attackDamage = GetComponent<EnemyStats>().damage;
    }

    public virtual void FixedUpdate()
    {
        if (attackCooldown > 0.0f)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
    }

    public virtual bool IsReady(Vector3 vectorToPlayer)
    {
        return attackCooldown <= 0.0f && vectorToPlayer.magnitude < AttackRange;
    }

    public virtual void Attack()
    {
        attackCooldown = AttackMaxCooldown;
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
                playerStats.TakeDamage(attackDamage);
            }
        }
    }

    protected virtual void RangeAttack()
    {
        Debug.Log($"Range attack from {this.name}");
        // This method instantiate the projectile given in projectile component
        // Instant the position and then let the object do what it have to
        var projectile = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
        projectile.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
