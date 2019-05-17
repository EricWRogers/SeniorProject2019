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
    public VideoClip sauClip;

    double studioLength;
    double gameLength;
    double sauLength;

    public string menuSceneName = "DollyMenu";

    private void Start()
    {
        studioLength = studioClip.length;
        gameLength = gameClip.length;
        sauLength = sauClip.length;
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

        videoPlayer.targetCameraAlpha = 1;
        videoPlayer.SetDirectAudioVolume(0, 1);

        videoPlayer.clip = sauClip;
        videoPlayer.Play();

        videoPlayerAlpha = videoPlayer.targetCameraAlpha;
        videoPlayerVolume = videoPlayer.GetDirectAudioVolume(0);

        yield return new WaitForSeconds((float)sauLength - 3);

        while (videoPlayer.targetCameraAlpha > 0)
        {
            videoPlayerAlpha -= speed;
            videoPlayerVolume -= speed;
            videoPlayer.targetCameraAlpha = videoPlayerAlpha;
            videoPlayer.SetDirectAudioVolume(0, videoPlayerVolume);
            yield return new WaitForSeconds(.1f);
        }

        SceneManager.LoadScene(menuSceneName);
    }
}
