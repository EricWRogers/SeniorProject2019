using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class HUD : MonoBehaviour
{
    public float reloadTime = 5.0f;
    public float tutTime = 5.0f;

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

    public GameObject tutCanvas;
    public Text tutText;
    CharacterController CharController;

    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        //loseCanvas = 
        //winCanvas = 
        text = GameObject.Find("HUD/MessageCanvas/MessageText");
        greenKey = GameObject.Find("HUD/KeyCardCanvas/GreenKey");
        purpleKey = GameObject.Find("HUD/KeyCardCanvas/PurpleKey");
        blueKey = GameObject.Find("HUD/KeyCardCanvas/BlueKey");
        CharController = (CharacterController)FindObjectOfType(typeof(CharacterController));
    }

    void Start()
    {
        messageForPlayer();
        PlayTutorial("test 1");
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
        //SceneManager.LoadScene(mainMenu);
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

    //public void PlayTutorialText(string text)
    //{
    //    float curTime = Time.time;
    //    float duration = 5;
    //    tutText.text = text;

    //    if(Time.time == curTime+duration)
    //    {
    //        tutText.text = "";
    //    }
    //}

    public void PlayTutorial(string text1)
    {
        StartCoroutine(PlayTutorialText(text1));
    }

    IEnumerator PlayTutorialText(string text1)
    {
        //movement tut
        tutText.text = "WASD to move";

        yield return new WaitWhile(() => CharController.velocity.magnitude <= 5f);
        yield return new WaitForSeconds(.5f);
        tutText.text = "Shift to sprint";
        //sprint tut

        yield return new WaitWhile(() => !InputManager.instance.Sprint());
        yield return new WaitForSeconds(.5f);
        tutText.text = "";
    }

    void FadeIn(Image i)
    {
        i.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut(Image i)
    {
        i.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}