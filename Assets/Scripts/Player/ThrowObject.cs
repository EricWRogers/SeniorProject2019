using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public string PickupMessage;
    public string ThrowMessage;

    public float throwForce;

    public Material material;
    public Vector3 Throwable;

    bool playerHolding = false;

    Transform player;
    Transform hand;
    HUD hud;
    Renderer rend;
    Material tempMaterial;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hand = player.transform.Find("Hand").transform;
        hud = GameObject.FindObjectOfType<HUD>();
        rend = GetComponent<Renderer>();

        playerHolding = false;
    }

    void Update()
    {
        if (playerHolding)
        {
            Pickup();
            Fire();
        }
        else
        {
            CheckIfPlayerHolding();
        }
    }

    void CheckIfPlayerHolding()
    {
        if (InputManager.instance.Interact() && !playerHolding)
        {
            playerHolding = true;
            GetComponent<Collider>().enabled = false;

        }
    }

    void Pickup()
    {
        transform.position = hand.transform.position;
    }

    void Fire()
    {
        if (InputManager.instance.Interact())
        {
            hud.MessageForPlayer();
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().AddForce(player.transform.forward * throwForce);
            playerHolding = false;
        }
    }

     void OnCollisionEnter (Collision other)
     {
        if(other.relativeVelocity.magnitude > 2f)
        {
            AudioManager.instance.PlayThisHere(transform.position, "Hit");
            
            if(other.relativeVelocity.magnitude >=2f)
            {   
                if (other.contactCount > 0) 
                {
                    Throwable = other.GetContact(0).point; 
                    StateController._throwObject = this;
                    Debug.Log("objects vector3"+other.transform.position);
                }
            }
        }
     }

    void OnMouseEnter()
    {
        tempMaterial = rend.material;
    }

    void OnMouseOver()
    {
        rend.material = material;

        if(!playerHolding)
        {
            hud.MessageForPlayer(PickupMessage);
        }
        else
        {
            hud.MessageForPlayer(ThrowMessage);
        }
    }

    void OnMouseExit()
    {
        rend.material = tempMaterial;

        if (playerHolding)
        {
            hud.MessageForPlayer(ThrowMessage);
        }
        else
        {
            hud.MessageForPlayer();
        }
    }
}