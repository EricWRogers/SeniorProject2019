using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] locationSounds;
    //public List<Sound> sounds = new List<Sound>();

    public new AudioClip[] audio;

    public static AudioManager instance;

    void Start()
    {
        //Play("Walking 1");
        //FindObjectOfType<AudioManager>().Play("Walking 1");
    }

    void Awake()
    {
        audio = Resources.LoadAll<AudioClip>("Audio/");
        //sounds = < Sound > audio;
        //sounds = new Sound[audio.Length];
        //int soundIndex = 0;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.outputMixerGroup;
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.playOnAwake = s.playOnAwake;
        }
        //foreach (AudioClip a in audio)
        //{
        //    //sounds[soundIndex].audioClip = a;
        //    sounds[soundIndex].name = "this";
        //    soundIndex++;
        //}
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void CreateAudioSource(string name, GameObject g)
    {
        Sound s = Array.Find(locationSounds, sound => sound.name == name);
        if(s.source == null)
        {
            s.source = g.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.outputMixerGroup;
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        //s.source.Play();
    }

    //public void CreateAudioS(AudioClip a, GameObject g, AudioMixerGroup mxg)
    //{
    //    AudioSource source;
    //    source = g.AddComponent<AudioSource>();
    //    source.clip = a;
    //    source.outputAudioMixerGroup = mxg;
    //}

    public void PlaySoundHere(string name)
    {
        Sound s = Array.Find(locationSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}