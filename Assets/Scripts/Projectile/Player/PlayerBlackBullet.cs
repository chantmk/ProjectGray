using UnityEngine;

public class PlayerBlackBullet : PlayerProjectile
{
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
        target.GetComponent<EnemyStats>().TakeDamage(damage);
        Destroy(gameObject);
    }
}