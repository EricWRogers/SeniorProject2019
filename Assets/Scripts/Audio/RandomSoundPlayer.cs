using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    float randomTime = 30;
    float timeCounter = 0;
    int randomSound = 0;
    
    void Update()
    {
        if(timeCounter > randomTime)
        {
            randomTime = Random.Range(10, 90);
            timeCounter = 0;
            audioSource.Stop();
            ChooseSound();
            audioSource.Play();
        }

        timeCounter += Time.deltaTime;
    }

    void ChooseSound()
    {
        randomSound = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomSound];
    }
}
