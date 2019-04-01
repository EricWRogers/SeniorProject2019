using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosive : MonoBehaviour
{
    GameObject playerGO;
    GameManager gameManagerGO;
    Text messageGO;

    public float maxDistance;
    public string message;
    public Material Default;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        gameManagerGO = GameObject.FindObjectOfType<GameManager>();
        messageGO = GameObject.Find("HUD").transform.Find("MessageCanvas").transform.Find("BottomMessageText").GetComponent<Text>();
    }

    void Update()
    {
        CheckExplosive();
    }

    void CheckExplosive()
    {
        float dist = Vector3.Distance(playerGO.transform.position, transform.position);

        if (dist <= maxDistance)
        {
            messageGO.text = message;

            if(InputManager.instance.Interact())
            {
                gameManagerGO.explosives--;
                messageGO.text = "";
                GetComponent<Renderer>().material = Default;
                GetComponent<Explosive>().enabled = false;
            }
        }
        else
        {
            messageGO.text = "";
        }
    }
}
