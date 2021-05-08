﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

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
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("Play");
        videoPlayer.Play();
    }

    public void StopVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}