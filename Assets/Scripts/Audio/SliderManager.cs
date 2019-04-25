using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMouseSensativity(float sensativity)
    {
        //change mouse sensativity
        //InputManager.instance.Looking_Horizontal.r
        //Input.GetAxis("Looking_Horizontal_Joystick_MacOSX") = sensativity;
        //Input.GetAxis("Looking_Horizontal_Joystick_Windows");
        //Input.GetAxis("looking_Horizontal_Joystick_Linux");
        //Input.GetAxis("Looking_Vertical_Joystick_MacOSX");
        //Input.GetAxis("Looking_Vertical_Joystick_Windows");
        //Input.GetAxis("looking_Vertical_Joystick_Linux");
        //Input.ge
        PlayerPrefs.SetFloat("MouseSensativity", sensativity);
    }
}

