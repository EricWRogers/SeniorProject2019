using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    private GameObject greenKey;
    private GameObject purpleKey;
    private GameObject blueKey;

    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        //loseCanvas = 
        //winCanvas = 
        text = GameObject.Find("HUD/MessageCanvas/MessageText");
        greenKey = GameObject.Find("HUD/KeyCardCanvas/GreenKey");
        purpleKey = GameObject.Find("HUD/KeyCardCanvas/PurpleKey");
        blueKey = GameObject.Find("HUD/KeyCardCanvas/BlueKey");
    }

    void Start()
    {
        messageForPlayer();
    }

    void Update()
    {
        //CalculateTimer();
        ShowKeyCard();
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

    public void ShowKeyCard()
    {
        List<string> KeysInPocket = FindObjectOfType<KeyChain>().KeysInPocket;

        //string keys = string.Join(",", KeysInPocket);
        //keyCardText.GetComponent<Text>().text = (string)KeysInPocket;

        if(KeysInPocket.Contains("Green"))
        {
            greenKey.SetActive(true);
            //keyCardText.GetComponent<Text>().text = "Green Key";
        }

        if (KeysInPocket.Contains("Purple"))
        {
            purpleKey.SetActive(true);
        }

        if (KeysInPocket.Contains("Blue"))
        {
            blueKey.SetActive(true);
        }
    }

}