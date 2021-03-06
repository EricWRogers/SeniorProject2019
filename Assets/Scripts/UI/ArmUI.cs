﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmUI : MonoBehaviour
{
    private int numOfAdrenaline;
    private int numOfExplosives;
    private int numOfKeys;

    private GameManager gameManager;
    private GameObject player;
    private GameObject armCanvas;

    private Animator animator;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        player = GameObject.FindGameObjectWithTag("Player");
        numOfKeys = player.GetComponent<KeyChain>().KeysInPocket.Count;

        numOfAdrenaline = gameManager.adrenaline;
        numOfExplosives = gameManager.explosives;
    }

    void Update()
    {
        CheckAdrenaline();
        CheckExplosives();
        CheckKeyCards();
    }

    void CheckAdrenaline()
    {
        if(numOfAdrenaline != gameManager.adrenaline)
        {
            numOfAdrenaline = gameManager.adrenaline;
            DisplayAdrenaline();
        }
        else
        {
            DisplayAdrenaline();
        }
    }

    void CheckExplosives()
    {
        if(numOfExplosives != gameManager.explosives)
        {
            numOfExplosives = gameManager.explosives;
            DisplayExplosives();
        }
        else
        {
            DisplayExplosives();
        }
    }

    void CheckKeyCards()
    {
        if(numOfKeys != player.GetComponent<KeyChain>().KeysInPocket.Count)
        {
            numOfKeys = player.GetComponent<KeyChain>().KeysInPocket.Count;
            DisplayKeys();
        }
    }

    void DisplayAdrenaline()
    {
        switch (numOfAdrenaline)
        {            
            case 0:

                if (transform.Find("Adrenaline") != null)
                {
                    transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(true);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_1").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                }
                break;

            case 1:

                if (transform.Find("Adrenaline") != null)
                {
                    transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_1").gameObject.SetActive(true);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                }
                break;

            case 2:

                if (transform.Find("Adrenaline") != null)
                {
                    transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_1").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(true);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                }
                break;

            case 3:

                if (transform.Find("Adrenaline") != null)
                {
                    transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_1").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                    transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(true);
                }
                break;

            default:
                Debug.Log("Adrenaline not Displaying");
                break;
        }
    }

    void DisplayExplosives()
    {
        switch (numOfExplosives)
        {
            case 0:

                if (transform.Find("Explosives") != null)
                {
                    transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(true);
                    transform.Find("Explosives").transform.Find("Explosives_1").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                }

                break;

            case 1:

                if (transform.Find("Explosives") != null)
                {
                    transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_1").gameObject.SetActive(true);
                    transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                }

                break;

            case 2:

                if (transform.Find("Explosives") != null)
                {
                    transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_1").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(true);
                    transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                }

                break;

            case 3:

                if (transform.Find("Explosives") != null)
                {
                    transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_1").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                    transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(true);
                }

                break;

            default:
                Debug.Log("Explosives not Displaying");
                break;
        }
    }

    void DisplayKeys()
    {
        for(int i = 0; i < player.GetComponent<KeyChain>().KeysInPocket.Count; ++i)
        {
            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "Blue")
            {
                transform.Find("Keys").transform.Find("Key_Blue").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "Green")
            {
                transform.Find("Keys").transform.Find("Key_Green").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "White")
            {
                transform.Find("Keys").transform.Find("Key_White").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "Purple")
            {
                transform.Find("Keys").transform.Find("Key_Purple").gameObject.SetActive(true);
            }
        }
    }
}
