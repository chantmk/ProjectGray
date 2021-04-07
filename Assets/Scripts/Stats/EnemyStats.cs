using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public GameObject dropItem;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        isDead = true;
        Debug.Log(transform.name + " Died");
        Instantiate(dropItem, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
