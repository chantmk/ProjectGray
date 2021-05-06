using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Projectile : MonoBehaviour
{
    protected abstract List<int> targetLayers { get; }

    public float damage = 10.0f;
    public float MaxDuration = 3.0f;
    public float FlightSpeed = 1.0f;

    protected float duration;
    protected AttackHitbox attackHitbox;
    protected Rigidbody2D projectileRigidbody;
    protected Animator animator;

    // Start is called before the first frame update
    public virtual void Start()
    {
        duration = MaxDuration;
        projectileRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        projectileRigidbody.velocity = direction * FlightSpeed;
    }

    protected virtual void Execute()
    {
        animator.SetTrigger(AnimatorParams.Execute);
    }

    public virtual void SelfDestruct()
    {
        Destroy(gameObject);
    }   
    
    protected abstract void Attack(GameObject target);
}
