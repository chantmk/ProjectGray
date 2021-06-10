using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Teleport : MonoBehaviour
{

    private static bool isEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        EventPublisher.DecisionMake += EnablePortal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventPublisher.DecisionMake -= EnablePortal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnable && other.gameObject.name == "Player")
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene("MapTransition");
            switch (PlayerConfig.CurrentScene)
            {
                case SceneEnum.BlackEnemyScene:
                    SceneManager.LoadScene("BlackBossScene");
                    break;
                case SceneEnum.BlackBossScene:
                    SceneManager.LoadScene("MapTransition");
                    break;
                case SceneEnum.BlueEnemyScene:
                    SceneManager.LoadScene("BlueBossScene");
                    break;
                case SceneEnum.BlueBossScene:
                    SceneManager.LoadScene("MapTransition");
                    break;
                case SceneEnum.YellowEnemyScene:
                    SceneManager.LoadScene("YellowBossScene");
                    break;
                case SceneEnum.YellowBossScene:
                    SceneManager.LoadScene("MapTransition");
                    break;
            }
        }
    }

    public void EnablePortal(DecisionEnum decision, CharacterNameEnum bossName)
    {
        isEnable = true;
    }

    public static void DisablePortal()
    {
        isEnable = false;
    }
}
