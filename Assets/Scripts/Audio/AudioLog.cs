using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLog : MonoBehaviour
{
    public enum AudioLogs
    {
        Amanda_11_1,
        Amanda_12_1,
        Amanda_14_1,
        Amanda_18_1,
        Amanda_20_1,
        Amanda_5_1,
        Amanda_9_1,
        Ben1,
        Ben3,
        Ben5,
        Ben6,
        Ben7,
        Ben8,
        Ben9,
        Blake_36_1,
        Blake_37_1,
        Blake_38_1,
        Blake_39_1,
        Blake_40_1,
        Blake_41_1,
        Blake_45_1,
        Courtney11,
        Courtney13,
        Courtney17,
        Courtney18,
        Courtney19,
        Courtney3,
        Courtney7,
        Courtney8,
        Petr10,
        Petr11,
        Petr12,
        Petr18,
        Petr2,
        Petr6,
        Petr9,
        Veronica_10_1,
        Veronica_11_1,
        Veronica_12_1,
        Veronica_14_1,
        Veronica_4_1,
        Veronica_6_1,
        Veronica_7_1
    };

    public AudioManager audioManager;

    public AudioLogs audioLogs;    

    string audioLog;

    private void Start()
    {
        audioLog = audioLogs.ToString();;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (InputManager.instance.Interact())
            {
                Destroy(gameObject);
                audioManager.Play(audioLog);
            }
        }
    }
}

