using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public string target = "Player";
    public float damage = 10.0f;
    public float MaxDuration = 3.0f;
    public float FlightSpeed = 1.0f;

    protected float duration;
    protected bool attacking => duration > 0.01f;
    protected AttackHitbox attackHitbox;
    protected Rigidbody2D mRigidbody;
    
    private bool attackFlag = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        duration = MaxDuration;
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // This method will decrease duration
        duration -= Time.fixedDeltaTime;
        if (attacking)
        {
            ProcessAttack();
        }
        else
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

    protected virtual void ProcessAttack()
    {
        HashSet<Collider2D> colliders = attackHitbox.HitColliders;
        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider.gameObject.tag == target && !attackFlag)
            {
                attackFlag = true;
                Attack(collider.gameObject);
            }
        }
    }
    protected abstract void Attack(GameObject target);
}
