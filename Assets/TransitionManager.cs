using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    private int beginPos;
    private int endPos;
    [SerializeField] float speed;
    [SerializeField] private float smoothDampSpeed;
    private bool isMoving;
    
    public GameObject playerIcon;

    private Vector3[] points;
    private Vector3[] playerPoints;
    private Vector3 vel = Vector3.zero;
    void Start()
    {
        points = new Vector3[]
        {
            new Vector3(0f, 21f, -10f),
            new Vector3(0f, 7f, -10f),
            new Vector3(0f, -7f, -10f),
            new Vector3(0f, -21f, -10f),
        };
        
        playerPoints = new Vector3[]
        {
            new Vector3(0f, 21f, 0f),
            new Vector3(0f, 7f, 0f),
            new Vector3(0f, -10f, 0f),
            new Vector3(0f, -21f, 0f),
        };

        transform.position = points[0];
        playerIcon.transform.position = playerPoints[0];
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            traverse(0,3);
        }

        move();
    }

    public void traverse(int begin, int end)
    {
        beginPos = begin;
        endPos = end;
        transform.position = points[begin];
        playerIcon.transform.position = playerPoints[begin];
        isMoving = true;
    }

    private void move()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, points[endPos], speed * Time.deltaTime);
            playerIcon.transform.position = Vector3.SmoothDamp(playerIcon.transform.position, playerPoints[endPos], ref vel, smoothDampSpeed * Time.deltaTime);
            
        }
        if (transform.position == points[endPos])
        {
            isMoving = false;
        }
    }
    
}
