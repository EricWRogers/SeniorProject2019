using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTimer : MonoBehaviour
{
    GameManager gameManager;
    GameObject text;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        text = GameObject.Find("HUD/HUD/TimerText");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTimer();
    }

    void CalculateTimer()
    {
        time = gameManager.TimerSet;

        float minutes = (int)time / 60;
        float seconds = (int)time % 60;
        float milSeconds = time - (minutes * seconds) - 60;

        if (time > 0)
        {
            text.GetComponent<Text>().text = string.Format("{0:0}:{1:00}{2:.000}", minutes, seconds, milSeconds);
        }
        else
        {
            text.GetComponent<Text>().text = "0:00.00";
        }
    }
}
