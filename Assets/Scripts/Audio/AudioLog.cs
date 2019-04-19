using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string audioLogMessage;

    string audioLog;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        hud = GameObject.FindObjectOfType<HUD>();
        audioLog = audioLogs.ToString();
        hud.AudioLogMessage(audioLogMessage);
        //audioManager.PlayAudioLog(audioLog);   test
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if (InputManager.instance.Interact())
            //{
                Destroy(gameObject);
                audioManager.PlayAudioLog(audioLog);
            //}
        }
    }
}

