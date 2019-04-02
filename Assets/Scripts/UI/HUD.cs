using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public float reloadTime = 5.0f;
    public float tutTime = 5.0f;

    public string mainMenu;
    public string startMessage;

    float time;

    Text topMessageText;
    Text bottomMessageText;
    Text timerText;

    GameManager GameManager;
    Canvas winCanvas;
    Canvas loseCanvas;
    CharacterController CharController;

    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        CharController = (CharacterController)FindObjectOfType(typeof(CharacterController));
        topMessageText = GameObject.Find("MessageCanvas").transform.Find("TopMessageText").gameObject.GetComponent<Text>();
        bottomMessageText = GameObject.Find("MessageCanvas").transform.Find("BottomMessageText").gameObject.GetComponent<Text>();
        timerText = GameObject.Find("MessageCanvas").transform.Find("TimerText").GetComponent<Text>();
        loseCanvas = GameObject.Find("LoseCanvas").GetComponent<Canvas>();
        winCanvas = GameObject.Find("WinCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        objectiveForPlayer(startMessage);
        PlayTutorial("test 1");
    }

    void Update()
    {

    }

    void FadeIn(Image i)
    {
        i.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut(Image i)
    {
        i.CrossFadeAlpha(0.0f, 2.5f, false);
    }

    void CalculateTimer()
    {
        timerText.enabled = true;

        time = GameManager.TimerSet;

        float minutes = (int)time / 60;
        float seconds = (int)time % 60;

        if (time > 0)
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }

    public void objectiveForPlayer()
    {
        topMessageText.text = "";
    }

    public void objectiveForPlayer(string message)
    {
        topMessageText.text = message;
    }

    public void MessageForPlayer()
    {
        bottomMessageText.text = "";
    }

    public void MessageForPlayer(string message)
    {
        bottomMessageText.text = message;
    }

    public void PlayTutorial(string text1)
    {
        StartCoroutine(PlayTutorialText(text1));
    }

    IEnumerator PlayTutorialText(string text1)
    {
        //movement tut
        MessageForPlayer("WASD to move");

        yield return new WaitWhile(() => CharController.velocity.magnitude <= 5f);
        yield return new WaitForSeconds(.5f);
        MessageForPlayer("Shift to sprint");
        //sprint tut

        yield return new WaitWhile(() => !InputManager.instance.Sprint());
        yield return new WaitForSeconds(.5f);
        MessageForPlayer();
    }
}