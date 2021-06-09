using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        
        PlayerConfig.CurrentScene = (SceneEnum)SceneManager.GetActiveScene().buildIndex;
    }
    void ChangeScene()
    {

    }

    public void HandleGameOver()
    {
        switch (PlayerConfig.CurrentScene)
        {
            case SceneEnum.BlackEnemyScene:
                SceneManager.LoadScene("GameOverBlack");
                break;
            case SceneEnum.BlackBossScene:
                SceneManager.LoadScene("GameOverBlack");
                break;
            case SceneEnum.BlueEnemyScene:
                SceneManager.LoadScene("GameOverBlue");
                break;
            case SceneEnum.BlueBossScene:
                SceneManager.LoadScene("GameOverBlue");
                break;
            case SceneEnum.YellowEnemyScene:
                SceneManager.LoadScene("GameOverYellow");
                break;
            case SceneEnum.YellowBossScene:
                SceneManager.LoadScene("GameOverYellow");
                break;
        }
        
    }
}
