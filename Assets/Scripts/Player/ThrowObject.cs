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

        //Debug.Log(dist);

        if (dist <= 10f && Input.GetKeyDown(KeyCode.G))
        {
            PlayerHolding = true;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }

        if (PlayerHolding)
        {
            transform.position = hand.transform.position;
        }

        if (PlayerHolding && Input.GetMouseButtonDown(0))
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(player.transform.forward * throwForce);
            PlayerHolding = false;
        }
    }
}
