using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public bool IsTargetPlayer;
    public bool IsTargetEnemy;
    public float damage;
    public float DurationRange;
    public float AreaOfEffectRange;

    private float Duration;
    // Start is called before the first frame update
    public virtual void Start()
    {
        Attack();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // This method will decrease duration
    }

    protected abstract void Attack();
}
