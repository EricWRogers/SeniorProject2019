﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioMixerGroup outputMixerGroup;
    public AudioClip audioClip;

    [Range(0f, .5f)]
    public float volume = .5f;
    [Range(.1f, 3f)]
    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;  

    public bool loop;
    //public bool playOnAwake;
}