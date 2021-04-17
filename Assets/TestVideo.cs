using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TestVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        EventPublisher.PlayCutscene += PlayVideo;
        videoPlayer.loopPointReached += StopVideo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    void StopVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.Stop();
    }
}
