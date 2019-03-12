using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject difficultyUI;
    public GameObject optionsMenuUI;
    public GameObject creditsUI;
    public string gameSceneName;
    public float creditDuration = 100f;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void PlayEasy()
    {
        Debug.Log("Loading " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 3;
    }

    public void PlayNormal()
    {
        Debug.Log("Loading " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 2;
    }

    public void PlayHard()
    {
        Debug.Log("Loading " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 1;
    }

    public void PlayHardcore()
    {
        Debug.Log("Loading " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 0;
    }

    public void LoadDifficulty()
    {
        difficultyUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void CloseDifficulty()
    {
        difficultyUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void LoadOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void LoadCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        Invoke("CloseCredits", creditDuration);
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
