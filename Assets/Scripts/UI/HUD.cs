using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public float reloadTime = 5.0f;

    private float time;

    public string mainMenu;
    public string message;

    private GameManager GameManager;
    public GameObject loseCanvas;
    public GameObject winCanvas;
    private GameObject text;

    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        //loseCanvas = 
        //winCanvas = 
        text = GameObject.Find("HUD/MessageCanvas/MessageText");
    }

    void Start()
    {
        messageForPlayer();
    }

    void Update()
    {
        //CalculateTimer();
    }

    void CalculateTimer()
    {
        time = GameManager.TimerSet;

        float minutes = (int)time / 60;
        float seconds = (int)time % 60;

        if (time > 0)
        {
            text.GetComponent<Text>().text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            text.GetComponent<Text>().text = "0:00";
        }
    }

    void messageForPlayer()
    {
        text.GetComponent<Text>().text = message;
    }

    IEnumerator ReloadLose()
    {
        loseCanvas.SetActive(true);
        Debug.Log("waiting");
        yield return new WaitForSeconds(reloadTime);
        //Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(mainMenu);
    }

    IEnumerator ReloadWin()
    {
        winCanvas.SetActive(true);
        Debug.Log("waiting");
        yield return new WaitForSeconds(reloadTime);
        //Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(mainMenu);
    }

    public void ReloadSceneLose()
    {
        StartCoroutine(ReloadLose());
    }

    public void ReloadSceneWin()
    {
        StartCoroutine(ReloadWin());
    }

}