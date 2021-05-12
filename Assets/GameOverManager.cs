using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Image GameOverImage;

    private Vector3 startingPosition;
    [SerializeField] private float speed;
    void Start()
    {
        // var tempGameOverImageColor = GameOverImage.color;
        // tempGameOverImageColor.a = 0f;
        // GameOverImage.color = tempGameOverImageColor;
        //
        // StartCoroutine(Fade(GameOverImage));
        startingPosition = GameOverImage.transform.position;
        GameOverImage.transform.position = startingPosition - new Vector3(0f, 400f, 0f);
    }

    // private IEnumerator Fade(Image image)
    // {
    //     var tempColor = image.color;
    //     while (tempColor.a <= 256f)
    //     {
    //         tempColor.a += 0.7f*Time.fixedDeltaTime;
    //         image.color = tempColor;
    //         yield return new WaitForFixedUpdate();
    //     }
    // }

    

    // Update is called once per frame
    void Update()
    {
        Translate(GameOverImage);
        if (Input.anyKeyDown && Time.timeSinceLevelLoad >= 0.2)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
    
    private void Translate(Image image)
    {
        if (GameOverImage.transform.position != startingPosition)
        {
            //GameOverImage.transform.Translate(0f,speed,0f);
            GameOverImage.transform.position = Vector3.Lerp(GameOverImage.transform.position, startingPosition, Time.deltaTime * speed);
        }
    }
}
