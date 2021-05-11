using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Projectile : MonoBehaviour
{
    protected abstract List<int> targetLayers { get; }

    public int damage = 1;
    public float MaxDuration = 3.0f;
    public float MaxFlightSpeed = 1.5f;
    public float MinFlightSpeed = 0.2f;

    protected float duration;
    protected AttackHitbox attackHitbox;
    protected Rigidbody2D projectileRigidbody;

    // Start is called before the first frame update
    public virtual void Start()
    {
        duration = MaxDuration;
        projectileRigidbody = GetComponent<Rigidbody2D>();
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
        attackHitbox.Enable();
        attackHitbox.OnHitboxTriggerEnter = OnHitboxTriggerEnter;
        attackHitbox.OnHitboxTriggerExit = OnHitboxTriggerExit;
    }

    protected virtual void OnHitboxTriggerEnter(Collider2D other)
    {
        var targetGameobject = other.gameObject;
        if (targetLayers != null && targetLayers.Contains(targetGameobject.layer))
        {
            Attack(targetGameobject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Wall") || other.gameObject.layer == LayerMask.NameToLayer("WallHitbox"))
        {
            Execute();
        }
    }

    protected virtual void OnHitboxTriggerExit(Collider2D other)
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        // This method will decrease duration
        duration -= Time.fixedDeltaTime;
        if (duration <= 0)
        {
            Execute();
        }
    }

    public virtual void Shoot(Vector2 direction)
    {
        if (projectileRigidbody == null)
        {
            projectileRigidbody = GetComponent<Rigidbody2D>();
        }
        projectileRigidbody.velocity = direction.normalized * Random.Range(MinFlightSpeed, MaxFlightSpeed);
    }

    protected virtual void Execute()
    {
        SelfDestruct();
    }

    public virtual void SelfDestruct()
    {
        Destroy(gameObject);
    }   
    
    protected abstract void Attack(GameObject target);
}
