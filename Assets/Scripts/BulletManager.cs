using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    public Vector2 direction;
    private Rigidbody2D rigidbody;
    
    void Start()
    {
        rigidbody.velocity = direction * bulletSpeed;
    }
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
