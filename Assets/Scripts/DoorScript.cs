using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim;
    public string MyLock =  "";

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("PlayerNearDoor", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<KeyChain>().KeysInPocket.Contains(MyLock))
            {
                anim.SetBool("PlayerNearDoor", true);
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
