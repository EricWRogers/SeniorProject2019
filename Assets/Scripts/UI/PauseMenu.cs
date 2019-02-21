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

    void Start()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
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
    }

    void Update()
    {
        CheckPause();
    }

    void CheckPause()
    {
        //Debug.Log(InputManager.instance.Pause());
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

    public void GameOver()
    {

    }

}
