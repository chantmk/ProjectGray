using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            new Vector3(0f, 21.11f, -10f),
            new Vector3(0f, 7.07f, -10f),
            new Vector3(0f, -7.26f, -10f),
            new Vector3(0f, -21.11f, -10f),
        };
        
        playerPoints = new Vector3[]
        {
            new Vector3(0f, 17.85f, 0f),
            new Vector3(0f, 2.73f, 0f),
            new Vector3(0f, -11.25f, 0f),
            new Vector3(0f, -24.5f, 0f),
        };
        
        setUp();
    }
    
    // void Update()
    // {
    //     // if (Input.GetMouseButtonDown(0))
    //     // {
    //     //     traverse(1,0);
    //     // }
    //
    //     move();
    // }

    private void FixedUpdate()
    {
        move();
    }

    private void setUp()
    {
        switch (PlayerConfig.CurrentScene)
        {
            case SceneEnum.MainMenuScene:
                traverse(0,3);
                break;
            case SceneEnum.BlackBossScene:
                traverse(3,2);
                break;
            case SceneEnum.BlueBossScene:
                traverse(2,1);
                break;
            case SceneEnum.YellowBossScene:
                traverse(1,1);
                break;
        }
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
        if (Vector3.Distance(transform.position ,points[endPos]) < 0.1f) //End Effect
        {
            isMoving = false;
            switch (PlayerConfig.CurrentScene)
            {
                case SceneEnum.MainMenuScene:
                    SceneManager.LoadScene("BlackEnemyScene");
                    break;
                case SceneEnum.BlackBossScene:
                    SceneManager.LoadScene("BlueEnemyScene");
                    break;
                case SceneEnum.BlueBossScene:
                    SceneManager.LoadScene("YellowEnemyScene");
                    break;
                case SceneEnum.YellowBossScene:
                    //To be continue scene...
                    SceneManager.LoadScene("ToBeContinueScene");
                    break;
            }
        }
    }
    
}
