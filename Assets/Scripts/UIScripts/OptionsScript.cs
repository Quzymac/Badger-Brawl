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
    [SerializeField] Toggle fullscreenToggle;

    void Start ()
    {
        m.value = PlayerPrefs.GetFloat("musicvolume");
        s.value = PlayerPrefs.GetFloat("sfxvolume");
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

    public void ToggleFullScreen()
    {
        fullscreenToggle.isOn = !fullscreenToggle.isOn;
    }

    //lånad från Brackeys
    public void setFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OpenPanel(GameObject canvas)  //opens the chosen gameobject and closes the optionpanel
    {
        canvas.SetActive(true);
        optionpanel.SetActive(false);
    }

    private void Update()
    {
        if ((optionpanel.activeInHierarchy || screenpanel.activeInHierarchy || soundpanel.activeInHierarchy) && Input.GetButtonDown("BackController"))
        {
           BackToOptions();
        }
    }
    public void BackToOptions()  //backbutton to the screen and sound panels
     { 
         screenpanel.SetActive(false);
         soundpanel.SetActive(false);
         optionpanel.SetActive(true);
     }

}











