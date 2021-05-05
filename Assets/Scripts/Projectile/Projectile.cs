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
    protected Rigidbody2D mRigidbody;
    
    private bool attackFlag = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        duration = MaxDuration;
        mRigidbody = GetComponent<Rigidbody2D>();

        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
        attackHitbox.Enable();
        attackHitbox.OnHitboxTriggerEnter = OnHitboxTriggerEnter;
    }

    protected virtual void OnHitboxTriggerEnter(Collider2D other)
    {
        var targetGameobject = other.gameObject;
        if (targetLayers != null && targetLayers.Contains(targetGameobject.layer))
        {
            Attack(targetGameobject);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // This method will decrease duration
        duration -= Time.fixedDeltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Shoot(Vector2 direction)
    {
        if (mRigidbody == null)
        {
            mRigidbody = GetComponent<Rigidbody2D>();
        }
        mRigidbody.velocity = direction * FlightSpeed;
    }
    
    protected abstract void Attack(GameObject target);
}
