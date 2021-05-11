using System;
using UnityEngine;

public class PlayerBlueBullet : PlayerProjectile
{

    [SerializeField] private float accerelation;
    private Vector2 directionCache;
    private bool isReachTerminalVelocity;
    public override void Start()
    {
        base.Start();
        MinFlightSpeed = Mathf.Pow(MinFlightSpeed, 2);
    }

    public override void Update()
    {
        base.Update();
        // projectileRigidbody.velocity += directionCache * (accerelation * Time.deltaTime);
    }

    public void FixedUpdate()
    {
        var velocity = projectileRigidbody.velocity;
        if (velocity.sqrMagnitude > MinFlightSpeed)
        {
            projectileRigidbody.velocity *= accerelation;
        }
        else if (!isReachTerminalVelocity)
        {
            projectileRigidbody.velocity = directionCache * MinFlightSpeed;
            isReachTerminalVelocity = true;
        }
        
    }

    public override void Shoot(Vector2 direction)
    {
        if (projectileRigidbody == null)
        {
            projectileRigidbody = GetComponent<Rigidbody2D>();
        }
        projectileRigidbody.velocity = direction * MaxFlightSpeed;
        directionCache = direction;
    }
    
    protected override void Attack(GameObject target)
    {
        target.GetComponent<CharacterStats>().TakeDamage(damage);
        EventPublisher.TriggerParticleSpawn(ParticleEnum.BlueBulletSplashParticle, transform.position);
        Destroy(gameObject);
    }
}