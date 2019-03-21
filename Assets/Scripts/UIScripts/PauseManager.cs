﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    //This code will change from GameScene to MainMenuScene
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScene");
        //selectPlayer.GetComponent<SeletingPlayer>().enabled = false;
    }

    public bool PauseMenu = false;


    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public GameObject ExitToMainMenuPanel;

    //this method will enable the pausepanel in Unity, set the timescale to 0 so the game will pause and then turn the PauseMenu bool to true 
    void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;

        PauseMenu = true;

    }

    //this method will disable the pausepanel in Unity, set the timescale to normal and then turn the PauseMenu bool to false
    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        
        PauseMenu = false;
    }

    // this code opens the optionpanel and closes the pausepanel

    public void OpenOption()
    {
        OptionsPanel.SetActive(true);
    }

    //this method close the optionpanel and opens the pausepanel
    public void OptionBack()
    {
        OptionsPanel.SetActive(false);    
    }

    //this method opens the panel that asks you 
    public void QuitGamePanel()
    {
        ExitToMainMenuPanel.SetActive(true);
    }

    public void DoNotQuit()
    {
        ExitToMainMenuPanel.SetActive(false);
    }

   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("StartGame"))) // when you click on the Escape button this code will check if PauseMenu is true or false, if it is false then the pausemenu will open
        {
            
            if (!PauseMenu)
            {
                PauseGame();
                
            }
        }
    }

}