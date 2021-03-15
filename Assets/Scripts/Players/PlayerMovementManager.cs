using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField] private float xSpeed = 2.0f;
    [SerializeField] private float ySpeed = 2.0f;
    private int inputX, inputY;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var rawInputX = Input.GetAxisRaw("Horizontal");
        var rawInputY = Input.GetAxisRaw("Vertical");
        inputX = rawInputX == 0 ? 0 : (int)Mathf.Sign(rawInputX);
        inputY = rawInputY == 0 ? 0 : (int)Mathf.Sign(rawInputY);
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector2(inputX * xSpeed, inputY * ySpeed);
    }
}
