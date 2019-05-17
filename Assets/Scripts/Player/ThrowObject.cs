using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public string PickupMessage;
    public string ThrowMessage;

    public float throwForce;

    public float notifyVelocityThreshold;

    public Material material;
    public Vector3 Throwable;

    bool playerHolding = false;

    Transform player;
    Transform hand;
    HUD hud;
    Renderer rend;
    Material tempMaterial;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hand = player.transform.Find("Hand").transform;
        hud = GameObject.FindObjectOfType<HUD>();
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();

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

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Throwable velocity: "  + rb.velocity.magnitude);

        if (rb.velocity.magnitude >= notifyVelocityThreshold)
        {
            AudioManager.instance.PlayThisHere(transform.position, "Hit");

            //Add PE Here
     
            if (other.contactCount > 0)
            {
                Throwable = other.GetContact(0).point;
                StateController._throwObject = this;
                Debug.Log("objects vector3" + other.transform.position);
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

        if (!playerHolding)
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