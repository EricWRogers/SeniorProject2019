using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public string mainMenuScene;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider MouseSensativitySlider;

    public AudioManager audioManager;

    public Animator animator;

    bool timerIsDisabled;

    public GameObject player;
    public GameObject messageCanvas;
    public AudioSource sanityWhispers;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //messageCanvas = GameObject.Find("MessageCanvas");
        //sanityWhispers = player.transform.Find("Sanity Whispers").gameObject.GetComponent<AudioSource>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        MouseSensativitySlider.value = PlayerPrefs.GetFloat("MouseSensativity", 1f);
    }

    public void Pause()
    {
        pauseCanvas.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        //messageCanvas.SetActive(false);

        //if(messageCanvas.Find("TimerText").GetComponent<Text>().enabled == false)
        //{
        //    timerIsDisabled = true;
        //}
        //else
        //{
        //    messageCanvas.Find("TimerText").GetComponent<Text>().enabled = false;
        //    timerIsDisabled = false;
        //}
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<AudioSource>().Pause();
        sanityWhispers.GetComponent<AudioSource>().Pause();
    }

    public void Resume()
    {
        animator.SetBool("Paused", false);
        pauseCanvas.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        //messageCanvas.SetActive(true);

        //if (!timerIsDisabled)
        //{
        //    messageCanvas.Find("TimerText").GetComponent<Text>().enabled = true;
        //}

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<AudioSource>().Play();
        sanityWhispers.GetComponent<AudioSource>().Play();
        //pauseMenuUI.SetActive(false);
        audioManager.Play("UIMenuSelect");
    }

    public void LoadOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        audioManager.Play("UIMenuSelect");
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        audioManager.Play("UIMenuSelect");
    }

    public void Hover()
    {
        audioManager.Play("UIMenuHover");
    }

    public void Exit()
    {
        SceneManager.LoadScene(mainMenuScene);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        audioManager.Play("UIMenuSelect");
    }

    void Update()
    {
        //CheckPause();
    }

    void CheckPause()
    {
        if (InputManager.instance.Pause())
        {
            if (pauseMenuUI.activeSelf)
            {
                Debug.Log("Resume");
                Resume();
                animator.SetTrigger("Pause");
                animator.SetBool("Paused", true);
            }
            else
            {
                Debug.Log("Pause");
                Pause();
                animator.SetBool("Paused", false);
            }
        }
    }
}
