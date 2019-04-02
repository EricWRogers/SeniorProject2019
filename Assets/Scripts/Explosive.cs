using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    bool showMessage;
    GameObject playerGO;
    GameManager gameManagerGO;
    HUD hud;

    public float maxDistance;
    public string message;
    public Material Default;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        gameManagerGO = GameObject.FindObjectOfType<GameManager>();
        hud = GameObject.FindObjectOfType<HUD>();

        showMessage = false;
    }

    void Update()
    {
        ShowExplosiveMessage();
    }

    void ShowExplosiveMessage()
    {
        float dist = Vector3.Distance(playerGO.transform.position, transform.position);

        if (dist <= maxDistance)
        {
            showMessage = true;
            hud.MessageForPlayer(message);

            PlaceExplosive();
        }
        else if(showMessage)
        {
            hud.MessageForPlayer();
            showMessage = false;
        }
    }

    void PlaceExplosive()
    {
        if (InputManager.instance.Interact())
        {
            gameManagerGO.explosives--;
            hud.MessageForPlayer();
            GetComponent<Renderer>().material = Default;
            GetComponent<Explosive>().enabled = false;
        }
    }
}
