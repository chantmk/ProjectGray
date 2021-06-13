using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private void Start()
    {
        if(videoPlayer == null)
        {
            videoPlayer = transform.parent.Find("VideoPlayer").GetComponent<VideoPlayer>();
        }
        videoPlayer.loopPointReached += StopVideo;
        Cursor.visible = true;
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play");
        videoPlayer.Play();
        PlayerConfig.HardCode(); //Fucking Hard code all stats to zero
    }

    public void StopVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
        SceneManager.LoadScene("MapTransition");
    }

    public void OnContinueButtonClicked()
    {
        SceneManager.LoadScene((int) PlayerConfig.CurrentScene);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
