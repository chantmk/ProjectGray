using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Weapon status")]
    public int AttackDamage = 1;
    public float AttackRange = 1.0f;
    [Range(0.0f, 1.0f)]
    public float AttackRatio = 0.5f;
    public float AttackMaxCooldown = 2.0f;
    [Header("Range parameter")]
    public bool IsRange = false;
    public GameObject ProjectileComponent;
    public int ProjectileCount = 1;
    [SerializeField]
    private float shootAngle = 30.0f;

    protected EnemyMovement enemyMovement;

    private AttackHitbox attackHitbox;
    private float attackCooldown;

    public AudioClip atkSound;
    public float atkVolume = 1f;
    protected AudioSource audioSrc;

    public virtual void Start()
    {
        InitializeHitbox();
        attackCooldown = AttackMaxCooldown;
        enemyMovement = GetComponent<EnemyMovement>();
        audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();
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
            targetGameObject.GetComponent<CharacterStats>().TakeDamage(AttackDamage);
        }
    }

    protected virtual void OnMeleeHitboxTriggerExit(Collider2D other)
    {
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
        audioSrc.PlayOneShot(atkSound, atkVolume);
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
        //Debug.Log($"Melee attack from {this.name}");
        attackHitbox.QuickEnable();
    }

    protected virtual void RangeAttack()
    {
        //Debug.Log($"Range attack from {this.name}");
        // This method instantiate the projectile given in projectile component
        // Instant the position and then let the object do what it have to
        for (int i=0; i < ProjectileCount; i++)
        {
            var projectile = Instantiate(ProjectileComponent, transform.position, Quaternion.Euler(Vector3.zero));
            var rotation = Quaternion.AngleAxis(Random.Range(-shootAngle, shootAngle), Vector3.up);
            projectile.GetComponent<Projectile>().Shoot(rotation * enemyMovement.GetVectorToPlayer().normalized);
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
