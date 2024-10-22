﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        EventPublisher.PlayCutscene += PauseBGM;
        EventPublisher.EndCutscene += ResumeBGM;
    }

    private void OnDestroy()
    {
        EventPublisher.PlayCutscene -= PauseBGM;
        EventPublisher.EndCutscene -= ResumeBGM;
    }

    public void PauseBGM()
    {
        AudioListener.pause = true;
        Destroy(GetComponent<AudioSource>());
        GetComponent<AudioSource>().Play();
    }

    public void ResumeBGM()
    {
        AudioListener.pause = false;
    }
}
