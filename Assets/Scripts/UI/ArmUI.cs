using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmUI : MonoBehaviour
{
    private int numOfAdrenaline;
    private int numOfExplosives;
    private int numOfKeys;

    private GameManager gameManager;
    private GameObject player;
    private GameObject armCanvas;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        player = GameObject.FindGameObjectWithTag("Player");
        armCanvas = player.transform.Find("PlayerArms_W_ctrls").transform.Find("Arm Canvas").gameObject;
        numOfKeys = player.GetComponent<KeyChain>().KeysInPocket.Count;
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
    }

    void CheckExplosives()
    {
        if(numOfExplosives != gameManager.explosives)
        {
            numOfExplosives = gameManager.explosives;
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
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(true);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_1").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                break;

            case 1:
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(true);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                break;

            case 2:
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(true);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(false);
                break;

            case 3:
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Adrenaline").transform.Find("Adrenaline_3").gameObject.SetActive(true);
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
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(true);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_1").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                break;

            case 1:
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(true);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                break;

            case 2:
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(true);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(false);
                break;

            case 3:
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_0").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_2").gameObject.SetActive(false);
                armCanvas.transform.Find("Explosives").transform.Find("Explosives_3").gameObject.SetActive(true);
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
                armCanvas.transform.Find("Keys").transform.Find("Key_Blue").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "Green")
            {
                armCanvas.transform.Find("Keys").transform.Find("Key_Green").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "White")
            {
                armCanvas.transform.Find("Keys").transform.Find("Key_White").gameObject.SetActive(true);
            }

            if(player.GetComponent<KeyChain>().KeysInPocket[i] == "Purple")
            {
                armCanvas.transform.Find("Keys").transform.Find("Key_Purple").gameObject.SetActive(true);
            }
        }
    }

    void DisplayHartBeat()
    {
        
    }
}
