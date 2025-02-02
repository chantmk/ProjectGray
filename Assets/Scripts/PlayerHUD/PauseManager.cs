﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool GamePaused = false;
    public static bool IsGameSlow = false;

    [SerializeField]
    private GameObject pauseMenu;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
        // if (Input.GetKeyDown(KeyCode.Minus))
        // {
        //     if (IsGameSlow)
        //     {
        //         ResumeSlow();
        //     }
        //     else
        //     {
        //         Slow(0.5f);
        //     }
        // }
    }

    private void Slow(float timescale)
    {
        Time.timeScale = timescale;
        IsGameSlow = true;
    }

    public static void ResumeTime()
    {
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Resume()
    {
        ResumeTime();
        pauseMenu.SetActive(false);
    }

    public void ResumeSlow()
    {
        Time.timeScale = 1f;
        IsGameSlow = false;
    }

    public static void PauseTime()
    {
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Pause()
    {
        PauseTime();
        pauseMenu.SetActive(true);
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

}
