using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip mainMenuTrack;
    public AudioClip chaseTrack;
    public AudioClip gameOverTrack;

    public AudioSource audioSource;

    public AudioClip[] audioClips;    

    public string mainMenuScene = "MainMenuScene";
    public string tutorialScene = "Tutorial";
    private string currentScene;

    private float fadeDuration = .05f;
    
    private int randomSound = 0;

    private GameObject player;
    private GameObject creature;
    private float creatureDistance = 1000;
    private float creatureSoundDistance = 145f;

    private void Awake()
    {
        player = GameObject.Find("Player");
        creature = GameObject.Find("TheEntity!");        
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        UpdatePlayerDistance();

        if (currentScene != mainMenuScene && currentScene !=tutorialScene)
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
        else if (currentScene != mainMenuScene && creatureDistance < creatureSoundDistance)
        {
            if (audioSource.isPlaying)
            {
                FadeOutCall(audioSource, fadeDuration);
            }

            audioSource.clip = chaseTrack;
            audioSource.Play();
            audioSource.loop = true;
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

    void UpdatePlayerDistance()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        if(creature == null)
        {
            creature = GameObject.Find("TheEntity!");
        }

        if(player != null && creature != null)
        {
            creatureDistance = Vector3.Distance(player.transform.position, creature.transform.position);
        }
    }

}
