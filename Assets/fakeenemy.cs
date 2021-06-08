using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class fakeenemy : MonoBehaviour
{
    private SpriteRenderer shadow;

    public GameObject bulletObject;

    private Vector3 origin = new Vector3(3f, 0f, 0f);

    public float degree = 0;

    public float speed;

    private int interval;

    private float r = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        degree += speed * Time.fixedDeltaTime;
        interval = MathUtils.Mod(interval + 1, 60);
        transform.position =
            origin + new Vector3(Mathf.Cos(Mathf.Deg2Rad * degree) * r, Mathf.Sin(Mathf.Deg2Rad * degree) * r, 0f);

        if (interval == 0)
        {
            var direction = ((Vector2) (origin - transform.position)).normalized;
            var bullet = Instantiate(bulletObject, transform.position, Quaternion.Euler(Vector3.zero));
            bullet.GetComponent<EnemyProjectile>().Shoot(direction);
        }
        
    }
}
