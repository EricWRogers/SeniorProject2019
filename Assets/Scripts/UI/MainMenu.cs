﻿using System.Collections;
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

    public string sceneToLaunch = "Tutorial";
    public float creditDuration = 100f;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider MouseSensativitySlider;

    public AudioManager audioManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        MouseSensativitySlider.value = PlayerPrefs.GetFloat("MouseSensativiy", 0.75f);
    }

    public void PlayEasy()
    {
        Debug.Log("Loading " + sceneToLaunch);
        SceneManager.LoadScene(sceneToLaunch);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 3;
        DifficultyManager.instance.escapeTime = 240f;
        DifficultyManager.instance.EntityRunSpeed = 22f;
        DifficultyManager.instance.EntityWalkSpeed =19f;
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void PlayNormal()
    {
        Debug.Log("Loading " + sceneToLaunch);
        SceneManager.LoadScene(sceneToLaunch);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 2;
        DifficultyManager.instance.escapeTime = 210f;
        DifficultyManager.instance.EntityRunSpeed = 28f;
        DifficultyManager.instance.EntityWalkSpeed =24f;
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void PlayHard()
    {
        Debug.Log("Loading " + sceneToLaunch);
        SceneManager.LoadScene(sceneToLaunch);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 1;
        DifficultyManager.instance.escapeTime = 180f;
        DifficultyManager.instance.EntityRunSpeed = 30f;
        DifficultyManager.instance.EntityWalkSpeed =26f;
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void PlayHardcore()
    {
        Debug.Log("Loading " + sceneToLaunch);
        SceneManager.LoadScene(sceneToLaunch);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        DifficultyManager.instance.adrenaline = 0;
        DifficultyManager.instance.escapeTime = 150f;
        DifficultyManager.instance.EntityRunSpeed = 35f;
        DifficultyManager.instance.EntityWalkSpeed =30f;
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void LoadDifficulty()
    {
        difficultyUI.SetActive(true);
        mainMenuUI.SetActive(false);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void CloseDifficulty()
    {
        difficultyUI.SetActive(false);
        mainMenuUI.SetActive(true);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void LoadOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void LoadCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        Invoke("CloseCredits", creditDuration);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void CloseCredits()
    {
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        AudioManager.instance.Play("UIMenuSelect");
    }

    public void Hover()
    {
        //audioManager.Play("UIMenuHover");
        AudioManager.instance.Play("UIMenuHover");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
