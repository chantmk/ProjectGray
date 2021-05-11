using UnityEngine;

public class PlayerBlackBullet : PlayerProjectile
{
    public override void Start()
    {
        base.Start();
        damage *= PlayerConfig.DamageMultiplier;
    }
    public override void Shoot(Vector2 direction)
    {
        if (projectileRigidbody == null)
        {
            projectileRigidbody = GetComponent<Rigidbody2D>();
        }
        projectileRigidbody.velocity = direction * MaxFlightSpeed;
    }
    
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
        EventPublisher.TriggerParticleSpawn(ParticleEnum.BlackBulletParticle, transform.position);
        Destroy(gameObject);
        
    }
}