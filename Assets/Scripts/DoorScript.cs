using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;
    public string neededKey =  "";
    public Component[] Light1;
    Color purpleDoor = Color.magenta;
    Color blueDoor = Color.blue;
    Color greenDoor = Color.green;
    Color redDoor = Color.red;
    public Color doorColor;
    AudioSource audioSource;
    public bool doorIsLocked;
    public bool doorIsBroken;
    public GameObject Sparks;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("PlayerNearDoor", false);
        if (neededKey == "Blue")
            doorColor = blueDoor;
        if (neededKey == "Green")
            doorColor = greenDoor;
        if (neededKey == "Purple")
            doorColor = purpleDoor;

        Light1 = gameObject.GetComponentsInChildren<Light>();
    
        foreach (Light light in Light1)
        {
            light.color = doorColor;
        }
        Sparks.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        doorIsLocked = false;
        if(doorIsBroken)
        {
            Sparks.SetActive(true);
            anim.SetBool("isBroken",true);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !doorIsLocked)
        {
            if(other.gameObject.GetComponent<KeyChain>().KeysInPocket.Contains(neededKey))
            {
                anim.SetBool("PlayerNearDoor", true);
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        
        }
        if ( other.gameObject.CompareTag("entity"))
        {
            anim.SetBool("PlayerNearDoor", true);            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("entity"))
        {         
            anim.SetBool("PlayerNearDoor", false);
        }
    }
}
