using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public enum ScreenColor
    {
        Blue,
        Green,
        Purple,
        White,
        None,
    }

    public enum DoorStates
    {
        Locked,
        Broken,
        Unlocked,
        
    }
    
    public ScreenColor screenColor;
    public DoorStates doorStates;

    public float lockOutTime;

    bool lockOveride;

    string neededKey;

    Color colorLight;
    Color colorScreen;
    AudioSource audioSource;
    Text lockedTimerText;
    Animator anim;

    void Start()
    {
        lockedTimerText = GameObject.Find("HUD").transform.Find("MessageCanvas").transform.Find("LockedTimerText").GetComponent<Text>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SetColorValue();
        CheckIfDoorIsBroken();
        CheckDoorLocks();
    }

    void SetColorValue()
    {
        switch(screenColor)
        {
            case ScreenColor.None:
                
                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Unlocked;
                    //colorScreen = Color.black;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    colorScreen = Color.black;
                }

                SetScreenColor();

                break;

            case ScreenColor.Blue:

                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Locked;
                    colorScreen = Color.blue;
                    neededKey = "Blue";
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    screenColor = ScreenColor.None;
                }

                SetScreenColor();
                break;

            case ScreenColor.Green:
            
                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Locked;
                    colorScreen = Color.green;
                    neededKey = "Green";
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    screenColor = ScreenColor.None;
                }

                SetScreenColor();
                break;

            case ScreenColor.Purple:
            
                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Locked;
                    colorScreen = Color.magenta;
                    neededKey = "Purple";
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    screenColor = ScreenColor.None;
                }

                SetScreenColor();

                break;

            case ScreenColor.White:
                colorScreen = Color.white;
                neededKey = "White";

                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Locked;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    screenColor = ScreenColor.None;
                }
                SetScreenColor();
                break;

            default:
                Debug.Log("No Color Selected For Door: " + gameObject.name);
                break;
        }
    }

    void CheckDoorLocks()
    {
        switch(doorStates)
        {
            case DoorStates.Unlocked:
                colorLight = Color.green;
                SetLightColor();
                break;

            case DoorStates.Broken:
                colorLight = Color.yellow;
                SetLightColor();
                break;

            case DoorStates.Locked:
                colorLight = Color.red;
                SetLightColor();
                break;
        }
    }

    void SetLightColor()
    {
        if( transform.Find("Light 1") != null)
        {
            transform.Find("Light 1").transform.Find("Lightbulb").GetComponent<Renderer>().material.SetColor("_EmissionColor", colorLight);
            transform.Find("Light 2").transform.Find("Lightbulb").GetComponent<Renderer>().material.SetColor("_EmissionColor", colorLight);
        }
    }

    void SetScreenColor()
    {
        if(transform.Find("DoorScreen 1") != null)
        {
            transform.Find("DoorScreen 1").transform.Find("Screen").GetComponent<Image>().color = colorScreen;
            transform.Find("DoorScreen 2").transform.Find("Screen").GetComponent<Image>().color = colorScreen;
        }
    }

    void CheckIfDoorIsBroken()
    {
        if(doorStates == DoorStates.Broken && transform.Find("Sparks").gameObject != null)
        {
            transform.Find("Sparks").gameObject.SetActive(true);
        }
    }

    void TempLockOut()
    {
        
    }

    void CalculateLockOutTimer()
    {
        lockedTimerText.enabled = true;

        float minutes = (int)lockOutTime / 60;
        float seconds = (int)lockOutTime % 60;

        if (lockOutTime > 0)
        {
            lockedTimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            lockedTimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorStates == DoorStates.Locked && screenColor != ScreenColor.None)
        {
            if(other.gameObject.GetComponent<KeyChain>().KeysInPocket.Contains(neededKey))
            {
                screenColor = ScreenColor.None;

                anim.SetBool("PlayerNearDoor", true);
                anim.SetTrigger("OpenDoor");

                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else if(other.gameObject.CompareTag("Player") && doorStates == DoorStates.Unlocked && screenColor == ScreenColor.None)
        {
            anim.SetBool("PlayerNearDoor", true);
            anim.SetTrigger("OpenDoor");

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if(other.gameObject.CompareTag("Player") && doorStates == DoorStates.Broken && screenColor == ScreenColor.None)
        {
            anim.SetTrigger("isBroken");
            anim.SetBool("PlayerNearDoor", true); 
        }

        else if (other.gameObject.CompareTag("entity"))
        {
            anim.SetBool("PlayerNearDoor", true);
            anim.SetTrigger("OpenDoor");
        }

        else if (other.gameObject.CompareTag("DollyCam"))
        {
            anim.SetBool("PlayerNearDoor", true);
            anim.SetTrigger("OpenDoor");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("entity"))
        {         
            anim.SetBool("PlayerNearDoor", false);
            anim.SetBool("isBroken", false);
        }
    }
}
