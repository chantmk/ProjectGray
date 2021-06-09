using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private float waitTime;

    void Update()
    {
        //Translate(GameOverImage);
        if (Input.anyKeyDown && Time.timeSinceLevelLoad >= waitTime)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}

