using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashVideoPlayer : MonoBehaviour
{
    public VideoPlayer studioPlayer;
    public VideoPlayer gamePlayer;

    private void Start()
    {
        FadeOutCall(studioPlayer, .05f);
    }

    void FadeOutCall(VideoPlayer videoPlayer, float speed)
    {
        StartCoroutine(FadeOut(videoPlayer, speed));
    }

    IEnumerator FadeOut(VideoPlayer videoPlayer, float speed)
    {
        float videoPlayerAlpha = videoPlayer.targetCameraAlpha;

        while (videoPlayer.targetCameraAlpha >= speed)
        {
            videoPlayerAlpha -= speed;
            videoPlayer.targetCameraAlpha = videoPlayerAlpha;
            yield return new WaitForSeconds(.1f);
        }
        //audioSource.Stop();
        videoPlayer.targetCameraAlpha = 1;
    }
}
