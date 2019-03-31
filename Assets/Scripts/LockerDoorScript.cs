using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDoorScript : MonoBehaviour
{
    Animator anim;
    AudioSource audioSource;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
    }
    
    
}
