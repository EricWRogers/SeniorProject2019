using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public float throwForce;
    public float distanceOffset = 15f;
    bool PlayerHolding = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.Find("Player/Hand");
        PlayerHolding = false;
        //GetComponent<Rigidbody>().isKinematic = false;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= distanceOffset && InputManager.instance.Interact() && !PlayerHolding)
        {
            PlayerHolding = true;
            //GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }

        if (PlayerHolding)
        {
            transform.position = hand.transform.position;
        }

        if (PlayerHolding && InputManager.instance.ThrowObject())
        {
            GetComponent<Collider>().enabled = true;
            //GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(player.transform.forward * throwForce);
            PlayerHolding = false;
        }
    }

     void OnCollisionEnter (Collision other)
     {
        if(other.relativeVelocity.magnitude > 2f)
        {
            AudioManager.instance.PlayThisHere(transform.position, "Hit");
        }
     }
}