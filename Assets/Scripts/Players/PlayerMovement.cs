using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float xSpeed = 2.0f;
    [SerializeField] private float ySpeed = 2.0f;
    private int inputX, inputY;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var rawInputX = Input.GetAxisRaw("Horizontal");
        var rawInputY = Input.GetAxisRaw("Vertical");
        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(inputX * xSpeed, inputY * ySpeed);
    }
}
