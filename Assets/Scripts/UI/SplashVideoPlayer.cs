using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public VideoClip studioClip;
    public VideoClip gameClip;

    double studioLength;
    double gameLength;

    public string menuSceneName = "DollyMenu";

    private void Start()
    {
        studioLength = studioClip.length;
        gameLength = gameClip.length;
        FadeOutCall(videoPlayer, .05f);                
    }

    void FadeOutCall(VideoPlayer videoPlayer, float speed)
    {
        StartCoroutine(FadeOut(videoPlayer, speed));
    }

    IEnumerator FadeOut(VideoPlayer videoPlayer, float speed)
    {
        float videoPlayerAlpha = videoPlayer.targetCameraAlpha;
        float videoPlayerVolume = videoPlayer.GetDirectAudioVolume(0);

        videoPlayer.Play();

        yield return new WaitForSeconds((float)studioLength - 3);

        while (videoPlayer.targetCameraAlpha >= speed || videoPlayer.GetDirectAudioVolume(0) > speed)
        {
            videoPlayerAlpha -= speed;
            videoPlayerVolume -= speed;
            videoPlayer.targetCameraAlpha = videoPlayerAlpha;
            videoPlayer.SetDirectAudioVolume(0, videoPlayerVolume);
            yield return new WaitForSeconds(.1f);
        }

        videoPlayer.targetCameraAlpha = 1;
        videoPlayer.SetDirectAudioVolume(0, 1);

        videoPlayer.clip = gameClip;
        videoPlayer.Play();

        videoPlayerAlpha = videoPlayer.targetCameraAlpha;
        videoPlayerVolume = videoPlayer.GetDirectAudioVolume(0);

        yield return new WaitForSeconds((float)gameLength);

        SceneManager.LoadScene(menuSceneName);
    }
}
