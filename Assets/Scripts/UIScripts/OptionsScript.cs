using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class OptionsScript : MonoBehaviour
{
    
    [SerializeField] GameObject optionpanel;
    [SerializeField] GameObject screenpanel;
    [SerializeField] GameObject soundpanel;
    [SerializeField] AudioMixer am; //our audiomixer from unity
    [SerializeField] Slider m;
    [SerializeField] Slider s;

    void Start ()
    {
        am.SetFloat("BGmusic", PlayerPrefs.GetFloat("musicvolume")); 
        am.SetFloat("Sfx", PlayerPrefs.GetFloat("sfxvolume"));
        m.value = PlayerPrefs.GetFloat("musicvolume");
        s.value = PlayerPrefs.GetFloat("sfxvolume");
        Debug.Log("ljud");
    }

    public void MusicVolume(float music) //this code is used on the gameobject "MusicSlider"
    {
        am.SetFloat("BGmusic", music);
        PlayerPrefs.SetFloat("musicvolume", music);
    }

    public void SFXVolume(float sfx) //this code is used on the gameobject "SFXSlider"
    {
        am.SetFloat("Sfx", sfx);
        PlayerPrefs.SetFloat("sfxvolume", sfx);
    }

    public void soundChosen()
    {
        soundpanel.SetActive(true);
        optionpanel.SetActive(false);
    }
    public void soundBack()
    {
        soundpanel.SetActive(false);
        optionpanel.SetActive(true);
    }

    public void screenChosen()
    {
        screenpanel.SetActive(true);
        optionpanel.SetActive(false);
    }
    public void screenBack()
    {
        screenpanel.SetActive(false);
        optionpanel.SetActive(true);
    }
   
    //lånad från Brackeys
    public void setFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }
}











