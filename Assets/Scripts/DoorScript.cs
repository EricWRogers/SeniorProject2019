using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public enum DoorColor
    {
        None,
        Blue,
        Green,
        Purple,
        White,
    }

    public enum DoorStates
    {
        Good,
        Broken,
        locked
    }

    public DoorStates doorStates;
    public DoorColor doorColor;

    string neededKey;

    Color colorLight;
    Color colorDoor;
    AudioSource audioSource;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        SetColorValue();
    }

    void Update()
    {
        CheckDoorLocks();
        CheckIfDoorIsBroken();
    }

    void SetColorValue()
    {
        switch(doorColor)
        {
            case DoorColor.None:
                
                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.Good;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                }

                SetScreenColor();

                break;

            case DoorColor.Blue:
                neededKey = "Blue";

                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.locked;
                    colorDoor = Color.blue;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    doorColor = DoorColor.None;
                }

                SetScreenColor();
                break;

            case DoorColor.Green:
                neededKey = "Green";

                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.locked;
                    colorDoor = Color.green;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    doorColor = DoorColor.None;
                }

                SetScreenColor();
                break;

            case DoorColor.Purple:
                neededKey = "Purple";
            
                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.locked;
                    colorDoor = Color.magenta;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    doorColor = DoorColor.None;
                }

                SetScreenColor();

                break;

            case DoorColor.White:
                colorDoor = Color.white;
                neededKey = "White";
                SetScreenColor();

                if(doorStates != DoorStates.Broken)
                {
                    doorStates = DoorStates.locked;
                }
                else
                {
                    doorStates = DoorStates.Broken;
                    doorColor = DoorColor.None;
                }

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
            case DoorStates.Good:
                colorLight = Color.green;
                SetLightColor();
                break;

            case DoorStates.Broken:
                colorLight = Color.yellow;
                SetLightColor();
                break;

            case DoorStates.locked:
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
            transform.Find("DoorScreen 1").transform.Find("Screen").GetComponent<Image>().color = colorDoor;
            transform.Find("DoorScreen 2").transform.Find("Screen").GetComponent<Image>().color = colorDoor;
        }
    }

    void CheckIfDoorIsBroken()
    {
        if(doorStates == DoorStates.Broken && transform.Find("Sparks").gameObject != null)
        {
            transform.Find("Sparks").gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorStates == DoorStates.locked && doorColor != DoorColor.None)
        {
            if(other.gameObject.GetComponent<KeyChain>().KeysInPocket.Contains(neededKey))
            {
                doorStates = DoorStates.Good;

                anim.SetBool("PlayerNearDoor", true);
                anim.SetTrigger("OpenDoor");

                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else if(other.gameObject.CompareTag("Player") && doorStates == DoorStates.Good && doorColor == DoorColor.None)
        {
            anim.SetBool("PlayerNearDoor", true);
            anim.SetTrigger("OpenDoor");

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if(other.gameObject.CompareTag("Player") && doorStates == DoorStates.Broken && doorColor == DoorColor.None)
        {
            anim.SetTrigger("isBroken");
            anim.SetBool("PlayerNearDoor", true); 
        }

        if ( other.gameObject.CompareTag("entity"))
        {
            anim.SetBool("PlayerNearDoor", true);           
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
