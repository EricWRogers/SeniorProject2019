using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip mainMenuTrack;
    public AudioClip gameOverTrack;

    public AudioSource audioSource;

    public AudioClip[] audioClips;    

    public string mainMenuScene = "MainMenuScene";
    private string currentScene;

    private float fadeDuration = .05f;
    
    private int randomSound = 0;

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != mainMenuScene)
        {
            if (!audioSource.isPlaying)
            {
                ChooseSound();
                audioSource.Play();
            }

            if(audioSource.clip == mainMenuTrack)
            {
                FadeOutCall(audioSource, fadeDuration);
                audioSource.loop = false;
            }
        }
        else
        {
            if (audioSource.clip != mainMenuTrack && audioSource.isPlaying)
            {
                FadeOutCall(audioSource, fadeDuration);
            }

            if (!audioSource.isPlaying)
            {
                audioSource.clip = mainMenuTrack;
                audioSource.Play();
                audioSource.loop = true;
            }            
        }
    }

    void ChooseSound()
    {
        randomSound = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomSound];
    }

    void FadeOutCall(AudioSource audioSource, float speed)
    {
        StartCoroutine(FadeOut(audioSource, speed));
    }

    IEnumerator FadeOut (AudioSource audioSource, float speed)
    {
        float audioVolume = audioSource.volume;

        while(audioSource.volume >= speed)
        {
            audioVolume -= speed;
            audioSource.volume = audioVolume;
            yield return new WaitForSeconds(.1f);
        }
        audioSource.Stop();
        audioSource.volume = 1;
    }

    void FadeInCall(AudioSource audioSource, float speed)
    {
        StartCoroutine(FadeIn(audioSource, speed));
    }

    IEnumerator FadeIn(AudioSource audioSource, float speed)
    {
        float audioVolume = audioSource.volume;

        while (audioSource.volume < 1)
        {
            audioVolume += speed;
            audioSource.volume = audioVolume;
            yield return new WaitForSeconds(.1f);
        }
    }
}
