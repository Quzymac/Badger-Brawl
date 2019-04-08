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

    
    public void MusicVolume(float music) //this code is used on the gameobject "MusicSlider"
    {
        am.SetFloat("BGmusic", music);
    }

    public void SFXVolume(float sfx) //this code is used on the gameobject "SFXSlider"
    {
        am.SetFloat("Sfx", sfx);
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











