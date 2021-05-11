using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum VideoState
{
    Play,
    Pause,
    Stop
}

public class VideoHandler : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.targetCamera = FindObjectOfType<Camera>();
        EventPublisher.PlayCutscene += PlayVideo;
        videoPlayer.loopPointReached += StopVideo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        EventPublisher.PlayCutscene -= PlayVideo;
        videoPlayer.loopPointReached -= StopVideo;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void StopVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
        EventPublisher.TriggerEndCutScene();
    }
}
