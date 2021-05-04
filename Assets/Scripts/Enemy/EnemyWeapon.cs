using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon status")]
    public float AttackDamage = 10.0f;
    public float AttackRange = 1.0f;
    [Range(0.0f, 1.0f)]
    public float AttackRatio = 0.5f;
    public float AttackMaxCooldown = 2.0f;
    public bool IsRange = false;
    public GameObject ProjectileComponent;
    public int ProjectileCount = 1;

    private AttackHitbox attackHitbox;
    private float attackCooldown;
    
    public virtual void Start()
    {
        InitializeHitbox();
        attackCooldown = AttackMaxCooldown;
    }

    private void InitializeHitbox()
    {
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
        attackHitbox.OnHitboxTriggerEnter = OnMeleeHitboxTriggerEnter;
        attackHitbox.OnHitboxTriggerExit = OnMeleeHitboxTriggerExit;
    }

    protected virtual void OnMeleeHitboxTriggerEnter(Collider2D other)
    {
        var targetGameObject = other.gameObject;
        if (targetGameObject.layer == LayerMask.NameToLayer("Player"))
        {
            targetGameObject.GetComponent<CharacterStats>().TakeDamage(attackDamage);
        }
    }

    protected virtual void OnMeleeHitboxTriggerExit(Collider2D other)
    {
    }

    public virtual void GetRelateComponent()
    {
        attackDamage = GetComponent<EnemyStats>().damage;
    }

    public virtual void FixedUpdate()
    {
        if (attackCooldown > GrayConstants.MINIMUM_TIME)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
    }

    public virtual bool IsReady(Vector3 vectorToPlayer)
    {
        return attackCooldown <= GrayConstants.MINIMUM_TIME && vectorToPlayer.magnitude < AttackRange;
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
        attackHitbox.QuickEnable();
    }

    protected virtual void RangeAttack()
    {
        Debug.Log($"Range attack from {this.name}");
        // This method instantiate the projectile given in projectile component
        // Instant the position and then let the object do what it have to
        for (int i=0; i < ProjectileCount; i++)
        {
            var projectile = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
            projectile.GetComponent<Projectile>().Shoot(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
