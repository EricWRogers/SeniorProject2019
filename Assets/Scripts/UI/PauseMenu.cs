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

    bool timerIsDisabled;

    GameObject player;
    Transform messageCanvas;
    AudioSource sanityWhispers;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        messageCanvas = GameObject.Find("HUD").transform.Find("MessageCanvas");
        sanityWhispers = player.transform.Find("Sanity Whispers").gameObject.GetComponent<AudioSource>();
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

        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        messageCanvas.Find("TopMessageText").GetComponent<Text>().enabled = false;
        messageCanvas.Find("BottomMessageText").GetComponent<Text>().enabled = false;

        if(messageCanvas.Find("TimerText").GetComponent<Text>().enabled == false)
        {
            timerIsDisabled = true;
        }
        else
        {
            messageCanvas.Find("TimerText").GetComponent<Text>().enabled = false;
            timerIsDisabled = false;
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<AudioSource>().Pause();
        sanityWhispers.GetComponent<AudioSource>().Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        messageCanvas.Find("TopMessageText").GetComponent<Text>().enabled = true;
        messageCanvas.Find("BottomMessageText").GetComponent<Text>().enabled = true;

        if (!timerIsDisabled)
        {
            messageCanvas.Find("TimerText").GetComponent<Text>().enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<AudioSource>().Play();
        sanityWhispers.GetComponent<AudioSource>().Play();
        //pauseMenuUI.SetActive(false);
    }

    public void LoadOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadScene(mainMenuScene);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
    }

    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        if (InputManager.instance.Pause())
        {
            if (pauseCanvas.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
