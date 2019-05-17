using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioLog : MonoBehaviour
{
    public enum AudioLogs
    {
        Amanda5,
        Amanda9,
        Amanda11,
        Amanda12,
        Amanda14,
        Amanda18,
        Amanda20,
        Ben1,
        Ben3,
        Ben5,
        Ben6,
        Ben7,
        Ben8,
        Ben9,
        Blake36,
        Blake37,
        Blake38,
        Blake39,
        Blake40,
        Blake41,
        Blake45,
        Cassandra1,
        Cassandra2,
        Cassandra4,
        Cassandra6,
        Cassandra11,
        Cassandra16,
        Cassandra18,
        Courtney3,
        Courtney7,
        Courtney8,
        Courtney11,
        Courtney13,
        Courtney17,
        Courtney18,
        Courtney19,
        Petr2,
        Petr6,
        Petr9,
        Petr10,
        Petr11,
        Petr12,
        Petr18,        
        Veronica4,
        Veronica6,
        Veronica7,
        Veronica10,
        Veronica11,
        Veronica12,
        Veronica14
    };

    AudioManager audioManager;
    HUD hud;

    public AudioLogs audioLogs;
    string audioLogName;

    string audioLog;
    string translatedAudioLog;
    string message = "Click to pickup audio log.";

    float playTime;
    float duration;

    bool played = false;
    bool pickupable = false;

    public Material material;

    Renderer rend;
    Material tempMaterial;
    Rigidbody rb;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        hud = GameObject.FindObjectOfType<HUD>();
        audioLog = audioLogs.ToString();
        audioLogName = new string(audioLog.Where(c => (c < '0' || c > '9')).ToArray());
        SpeakerName(audioLogName);

        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        tempMaterial = rend.material;
        //audioManager.PlayAudioLog(audioLog);   test
    }

    private void Update()
    {
        if(played)
        {
            if (Time.time > playTime + duration)
            {
                hud.AudioLogMessage();
                Destroy(gameObject);
            }
        }

        Pickup();

        if(audioManager == null)
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        if (InputManager.instance.Interact())
    //        {
    //            gameObject.GetComponent<MeshRenderer>().enabled = false;
    //            audioManager.PlayAudioLog(audioLog);
    //            hud.AudioLogMessage(translatedAudioLog);

    //            playTime = Time.time;
    //            played = true;
    //            duration = audioManager.AudioLogLength(audioLog);
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        if (InputManager.instance.Interact() && pickupable)
    //        {
    //            gameObject.GetComponent<MeshRenderer>().enabled = false;
    //            audioManager.PlayAudioLog(audioLog);
    //            hud.AudioLogMessage(translatedAudioLog);

    //            playTime = Time.time;
    //            played = true;
    //            duration = audioManager.AudioLogLength(audioLog);
    //        }
    //    }
    //}

    void Pickup()
    {
        if (InputManager.instance.Interact() && pickupable)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            audioManager.PlayAudioLog(audioLog);
            hud.AudioLogMessage(translatedAudioLog);

            playTime = Time.time;
            played = true;
            duration = audioManager.AudioLogLength(audioLog);
        }
    }

    void OnMouseEnter()
    {
        tempMaterial = rend.material;
        hud.MessageForPlayer(message);
        pickupable = true;
    }

    void OnMouseOver()
    {
        rend.material = material;
        hud.MessageForPlayer(message);
        pickupable = true;
    }

    void OnMouseExit()
    {
        rend.material = tempMaterial;
        hud.MessageForPlayer();
        pickupable = false;
    }

    void SpeakerName(string audioLogName)
    {
        switch(audioLogName)
        {
            case "Amanda":
                translatedAudioLog = "Dr. Brigette Weatherby";
                break;
            case "Ben":
                translatedAudioLog = "Dr. Adam Lowell";
                break;
            case "Blake":
                translatedAudioLog = "Dr. Jameson Frost";
                break;
            case "Cassandra":
                translatedAudioLog = "Dr. Amanda Cerny";
                break;
            case "Courtney":
                translatedAudioLog = "Dr. Sarah Long";
                break;
            case "Petr":
                translatedAudioLog = "Dr. Adrian Sokolov";
                break;
            case "Veronica":
                translatedAudioLog = "Dr. Mariana Flores";
                break;
            default:
                translatedAudioLog = "";
                break;
        }
    }
}

