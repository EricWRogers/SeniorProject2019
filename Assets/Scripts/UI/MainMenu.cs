using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject creditsUI;
    public string gameSceneName;
    public float creditDuration = 100f;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    private void Awake()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        //audioManager.CreateAudioSource("droppedObject",gameObject);
        Vector3 v3 = new Vector3(10f, 0f, 0f);
        audioManager.PlayThisHere(v3, "droppedObject", 1);
        //audioManager.PlayThisHere(transform.position, "droppedObject", 1);
        //FindObjectOfType<AudioManager>().CreateAudioSource(AudioManager.locationSounds = droppedObject,gameObject);
        //FindObjectOfType<AudioManager>().CreateAudioS(BuzzingLight,gameObject, SFX);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    public void Play()
    {
        Debug.Log("Loading " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
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
