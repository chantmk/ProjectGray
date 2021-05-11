using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Image GameOverImage;
    void Start()
    {
        var tempGameOverImageColor = GameOverImage.color;
        tempGameOverImageColor.a = 0f;
        GameOverImage.color = tempGameOverImageColor;

        StartCoroutine(Fade(GameOverImage));
    }

    private IEnumerator Fade(Image image)
    {
        var tempColor = image.color;
        while (tempColor.a <= 256f)
        {
            tempColor.a += 0.7f*Time.fixedDeltaTime;
            image.color = tempColor;
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && Time.timeSinceLevelLoad >= 0.2)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
