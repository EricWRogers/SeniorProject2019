using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public string startMessage;

    float gameOverTime;
    float lockOutTime;

    bool basics = true;

    Text topMessageText;
    Text bottomMessageText;
    Text tutorialMessageText;
    Text lockedTimerText;
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
        tutorialMessageText = GameObject.Find("MessageCanvas").transform.Find("TutorialMessageText").gameObject.GetComponent<Text>();
        timerText = GameObject.Find("MessageCanvas").transform.Find("TimerText").GetComponent<Text>();
        lockedTimerText = GameObject.Find("HUD").transform.Find("MessageCanvas").transform.Find("LockedTimerText").GetComponent<Text>();
        loseCanvas = GameObject.Find("LoseCanvas").GetComponent<Canvas>();
        winCanvas = GameObject.Find("WinCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        objectiveForPlayer(startMessage);
        PlayTutorial(basics);
        basics = false;
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

    public void CalculateTimer()
    {
        timerText.enabled = true;

        gameOverTime = GameManager.TimerSet;

        float minutes = (int)gameOverTime / 60;
        float seconds = (int)gameOverTime % 60;

        if (gameOverTime > 0)
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

    public void TutorialForPlayer()
    {
        bottomMessageText.text = "";
    }

    public void TutorialForPlayer(string message)
    {
        tutorialMessageText.text = message;
    }

    public void PlayTutorial(bool basics)
    {
        StartCoroutine(PlayTutorialText(basics));
    }    

    IEnumerator PlayTutorialText(bool basics)
    {
        float textRemoval = .5f;

        if (basics)
        {
            //movement tut
            TutorialForPlayer("WASD to move");
            yield return new WaitWhile(() => CharController.velocity.magnitude <= 5f);
            yield return new WaitForSeconds(textRemoval);

            //sprint tut
            TutorialForPlayer("Shift to sprint");
            yield return new WaitWhile(() => !InputManager.instance.Sprint());
            yield return new WaitForSeconds(textRemoval);

            //lean tut
            TutorialForPlayer("QE to lean");
            yield return new WaitWhile(() => !InputManager.instance.Lean_L() && !InputManager.instance.Lean_R());
            yield return new WaitForSeconds(textRemoval);

            //crouch tut
            TutorialForPlayer("Ctrl to crouch");
            yield return new WaitWhile(() => !InputManager.instance.Crouch());
            yield return new WaitForSeconds(textRemoval);

            //interact tut?

            //find scientist
            yield return new WaitForSeconds(textRemoval);
            TutorialForPlayer("Find and plant explosives");
            yield return new WaitWhile(() => GameManager.explosives > 1);

            //run when finding monster 5s

            //find each keycard

            //if explosives found
            //plant explosives instructions, not location

            //escape post planting explosives

            MessageForPlayer();
        }
        else
        {
            //find scientist
            yield return new WaitForSeconds(textRemoval);
            TutorialForPlayer("Find the Scientist");

            //run when finding monster 5s

            //find each keycard

            //if explosives found
            //plant explosives instructions, not location

            //escape post planting explosives

            MessageForPlayer();
        }        
    }
}