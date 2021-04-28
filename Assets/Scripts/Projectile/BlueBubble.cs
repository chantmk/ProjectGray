using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBubble : Projectile
{
    [SerializeField]
    private bool isStunt = false;
    [SerializeField]
    private float stuntDuration = 1.0f;
    protected override void Attack(GameObject target)
    {
        if (isStunt)
        {
            Debug.Log("Player stunt for " + stuntDuration);
        }
        else
        {
            target.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
