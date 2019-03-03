using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("PlayerNearDoor", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("entity"))
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
