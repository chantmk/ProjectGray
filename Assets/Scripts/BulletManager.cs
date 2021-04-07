using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public Vector2 direction;
    public float ShootingRange;
    private Rigidbody2D rigidbody;
    
    // TODO
    public float shaky;

    public float damage = 5f;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rigidbody.velocity = direction * bulletSpeed;
        rigidbody.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);

        // Mocking range
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if (shaky > 0f)
        {
            transform.position += (Vector3)(Vector2.Perpendicular(direction) * ((Random.value - 0.5f) * shaky));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && other.TryGetComponent<CharacterStats>(out CharacterStats stats))
        {
            stats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
