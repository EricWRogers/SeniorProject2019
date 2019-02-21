using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject loseCanvas;
    public GameObject winCanvas;
    private GameManager GameManager;
    private GameObject text;
    private float time;

    public string mainMenu;
    public float reloadTime = 5.0f;

    public void ReloadSceneLose()
    {
        StartCoroutine(ReloadLose());
    }

    public void ReloadSceneWin()
    {
        StartCoroutine(ReloadWin());
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

    private void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        text = GameObject.Find("HUD/TimerCanvas/TimerText");
    }

    void Update()
    {
        /*test
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("reloading");
            ReloadSceneLose();
        }
        */

        CalculateTimer();
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
}