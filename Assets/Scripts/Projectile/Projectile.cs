using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public LayerMask target;
    public float damage = 10.0f;
    public float MaxDuration = 3.0f;

    protected float duration;
    protected bool attacking => duration > 0.01f;
    private AttackHitbox attackHitbox;

    // Start is called before the first frame update
    public virtual void Start()
    {
        duration = MaxDuration;
        attackHitbox = transform.Find("AttackHitbox").GetComponent<AttackHitbox>();
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

    protected virtual void ProcessAttack()
    {
        HashSet<Collider2D> colliders = attackHitbox.HitColliders;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Attack(collider.gameObject);
            }
        }
    }
    protected abstract void Attack(GameObject target);
}
