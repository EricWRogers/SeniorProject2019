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
        if(GameManager.TimerSet < 0)
        {
            ReloadLose();
        }
    }
}