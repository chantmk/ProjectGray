using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Projectile
{
    public float PullStrength = 2.0f;

    protected override void Attack(GameObject target)
    {
        //target.transform.position = Vector2.MoveTowards(target.transform.position, transform.position, PullStrength * Time.deltaTime);
        target.GetComponent<Rigidbody2D>().AddForce((transform.position - target.transform.position) * PullStrength);
    }
}
