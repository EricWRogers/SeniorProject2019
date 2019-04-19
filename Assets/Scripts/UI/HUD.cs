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
    float fadeOutTime = 1f;

    bool basics = true;

    Text topMessageText;
    Text bottomMessageText;
    Text tutorialMessageText;
    Text audioLogText;
    Text lockedTimerText;
    Text timerText;

    GameManager GameManager;
    Canvas winCanvas;
    Canvas loseCanvas;
    CharacterController CharController;

    private GameObject player;
    private GameObject creature;
    private float creatureDistance;

    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        CharController = (CharacterController)FindObjectOfType(typeof(CharacterController));
        topMessageText = GameObject.Find("MessageCanvas").transform.Find("TopMessageText").gameObject.GetComponent<Text>();
        bottomMessageText = GameObject.Find("MessageCanvas").transform.Find("BottomMessageText").gameObject.GetComponent<Text>();
        tutorialMessageText = GameObject.Find("MessageCanvas").transform.Find("TutorialMessageText").gameObject.GetComponent<Text>();
        audioLogText = GameObject.Find("MessageCanvas").transform.Find("AudioLogText").gameObject.GetComponent<Text>();
        timerText = GameObject.Find("MessageCanvas").transform.Find("TimerText").GetComponent<Text>();
        lockedTimerText = GameObject.Find("HUD").transform.Find("MessageCanvas").transform.Find("LockedTimerText").GetComponent<Text>();
        loseCanvas = GameObject.Find("LoseCanvas").GetComponent<Canvas>();
        winCanvas = GameObject.Find("WinCanvas").GetComponent<Canvas>();
        player = GameObject.Find("Player");
        creature = GameObject.Find("Creature");
    }

    void Start()
    {
        objectiveForPlayer("");
        AudioLogMessage();
        PlayTutorial(basics);
        basics = false;
    }

    void Update()
    {
        if(player!= null && creature != null)
        {
            creatureDistance = Vector3.Distance(player.transform.position, creature.transform.position);
        }        
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

        gameOverTime = GameManager.escapeTime;

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

    public void AudioLogMessage()
    {
        audioLogText.text = "";
    }

    public void AudioLogMessage(string message)
    {
        audioLogText.text = message;
    }

    public void PlayTutorial(bool basics)
    {
        StartCoroutine(PlayTutorialText(basics));
    }

    public void FadeOut(Text fadeOutText)
    {
        StartCoroutine(FadeOutRoutine(fadeOutText));
    }

    private IEnumerator FadeOutRoutine(Text fadeOutText)
    {
        Text text = fadeOutText;
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));            
            yield return null;
        }
        text.text = "";
        text.color = originalColor;
    }

    IEnumerator PlayTutorialText(bool basics)
    {
        float textRemoval = .5f;
        float objectiveRemoval = 2.5f;

        if (basics)
        {
            //movement tut
            TutorialForPlayer("WASD to move");
            yield return new WaitForSeconds(textRemoval);
            yield return new WaitWhile(() => CharController.velocity.magnitude <= 5f);
            FadeOut(tutorialMessageText);
            yield return new WaitForSeconds(fadeOutTime);

            //sprint tut
            TutorialForPlayer("Shift to sprint");
            yield return new WaitWhile(() => !InputManager.instance.Sprint());
            FadeOut(tutorialMessageText);
            yield return new WaitForSeconds(fadeOutTime);

            //lean tut
            TutorialForPlayer("QE to lean");
            yield return new WaitWhile(() => !InputManager.instance.Lean_L() && !InputManager.instance.Lean_R());
            FadeOut(tutorialMessageText);
            yield return new WaitForSeconds(fadeOutTime);

            //crouch tut
            TutorialForPlayer("Ctrl to crouch");
            yield return new WaitWhile(() => !InputManager.instance.Crouch());
            FadeOut(tutorialMessageText);
            yield return new WaitForSeconds(fadeOutTime);

            //interact tut?

            //find keycards
            //TutorialForPlayer("Find Keycards to access more of the lab");
            //yield return new WaitWhile

            //Search the station and place explosives in key points
            TutorialForPlayer("Search the station and place explosives in key points");
            yield return new WaitForSeconds(objectiveRemoval);
            FadeOut(tutorialMessageText);
            objectiveForPlayer("Search the station and place explosives in key points");
            yield return new WaitWhile(() => GameManager.explosives > 1);
            //yield return new WaitForSeconds(textRemoval);

            //escape post planting explosives
            TutorialForPlayer("Escape!");
            yield return new WaitForSeconds(objectiveRemoval);
            FadeOut(tutorialMessageText);
            objectiveForPlayer("Escape!");
        }
        else
        {
            //Search the station and place explosives in key points
            TutorialForPlayer("Search the station and place explosives in key points");
            yield return new WaitForSeconds(objectiveRemoval);
            FadeOut(tutorialMessageText);
            objectiveForPlayer("Search the station and place explosives in key points");
            yield return new WaitWhile(() => GameManager.explosives > 1);
            //yield return new WaitForSeconds(textRemoval);

            //escape post planting explosives
            TutorialForPlayer("Escape!");
            yield return new WaitForSeconds(objectiveRemoval);
            FadeOut(tutorialMessageText);
            objectiveForPlayer("Escape!");
        }        
    }
}