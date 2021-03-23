using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public Vector2 direction;
    public float ShootingRange;
    private Rigidbody2D rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rigidbody.velocity = direction * bulletSpeed;
        rigidbody.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);

        // Mocking range
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
