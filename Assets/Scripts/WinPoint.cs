using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.Win();
        }
    }
}
