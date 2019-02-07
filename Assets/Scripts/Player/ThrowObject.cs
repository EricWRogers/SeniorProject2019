using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject player;
    public GameObject hand;
    public float throwForce;
    bool PlayerHolding = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hand = GameObject.Find("/Player/Hand");

        PlayerHolding = false;
        GetComponent<Rigidbody>().isKinematic = false;

    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist <= 2.5f && Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            PlayerHolding = true;
            GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
        }

        if (PlayerHolding)
        {
            transform.position = hand.transform.position;
        }

        if (PlayerHolding && Input.GetMouseButtonDown(0))
        {
            GetComponent<Collider>().enabled = !GetComponent<Collider>().enabled;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(player.transform.forward * throwForce);
            PlayerHolding = false;
        }
    }
}
