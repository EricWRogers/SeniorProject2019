using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;
    public string MyLock =  "";
    public Component[] Light1;
 
    Color purpleDoor = Color.magenta;
    Color blueDoor = Color.blue;
    Color greenDoor = Color.green;
    Color redDoor = Color.red;
    public Color doorColor;
    AudioSource audioSource;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("PlayerNearDoor", false);
        if (MyLock == "Blue")
            doorColor = blueDoor;
        if (MyLock == "Green")
            doorColor = greenDoor;
        if (MyLock == "Purple")
            doorColor = purpleDoor;

        Light1 = gameObject.GetComponentsInChildren<Light>();

        Debug.Log(Light1.Length);
        foreach (Light light in Light1)
        {
            light.color = doorColor;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<KeyChain>().KeysInPocket.Contains(MyLock))
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
