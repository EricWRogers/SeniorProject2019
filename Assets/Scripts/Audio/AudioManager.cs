using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] locationSounds;
    public Sound[] audioLogs;
    //public List<Sound> sounds = new List<Sound>();
    //public new AudioClip[] audio;

    public static AudioManager instance;

    public AudioSource voiceSource;

    private GameObject emptySpawn;

    void Start()
    {
        //Play("Walking 1");
        //FindObjectOfType<AudioManager>().Play("Walking 1");
        //voiceSource = gameObject.AddComponent<AudioSource>();
    }

    void Awake()
    {
        //audio = Resources.LoadAll<AudioClip>("Audio/");
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
            s.source.playOnAwake = s.playOnAwake;
        }
        //foreach (AudioClip a in audio)
        //{
        //    //sounds[soundIndex].audioClip = a;
        //    sounds[soundIndex].name = "this";
        //    soundIndex++;
        //}
    }

    //plays sound from sounds[]
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

    //creates audio source on object g for a sound from locationSounds[]
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
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    //plays sound from locationSounds[]
    public void PlayLocationSound(string name)
    {
        Sound s = Array.Find(locationSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if(!s.source.isPlaying)
            s.source.Play();
    }

    public void PlayAudioLog(string name)
    {
        if(voiceSource == null)
        {
            //voiceSource = gameObject.AddComponent<AudioSource>();
        }        

        Sound s = Array.Find(audioLogs, sound => sound.name == name);
        if (s.source == null)
        {
            s.source = voiceSource;
            s.source.outputAudioMixerGroup = s.outputMixerGroup;
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public float AudioLogLength(string name)
    {
        Sound s = Array.Find(audioLogs, sound => sound.name == name);
        return s.source.clip.length;
    }

    //creates empty object at v3 and plays sound from locationSounds[] 
    public void PlayThisHere(Vector3 v3, string name, float volume)
    {
        Sound s = Array.Find(locationSounds, sound => sound.name == name);
        emptySpawn = new GameObject(name + " sound");
        emptySpawn.transform.parent = instance.transform;
        //Instantiate(emptySpawn);
        //GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        emptySpawn.transform.position = v3;
        
        s.source = emptySpawn.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = s.outputMixerGroup;
        s.source.clip = s.audioClip;

        s.source.volume = volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.playOnAwake = s.playOnAwake;

        s.source.Play();
        Destroy(emptySpawn, s.audioClip.length);
    }

    //creates empty object at v3 and plays sound from locationSounds[] without volume
    public void PlayThisHere(Vector3 v3, string name)
    {
        Sound s = Array.Find(locationSounds, sound => sound.name == name);
        emptySpawn = new GameObject(name + " sound");
        emptySpawn.transform.parent = instance.transform;
        emptySpawn.transform.position = v3;

        s.source = emptySpawn.AddComponent<AudioSource>();
        s.source.outputAudioMixerGroup = s.outputMixerGroup;
        s.source.clip = s.audioClip;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.playOnAwake = s.playOnAwake;
        s.source.spatialBlend = 1;

        s.source.Play();
        Destroy(emptySpawn, s.audioClip.length); ;
    }
}

//using audio manager
//create an audio manager or just find each time
//AudioManager audioManager = FindObjectOfType<AudioManager>();
//audioManager.PlayThisHere(); || //FindObjectOfType<AudioManager>().PlayThisHere();