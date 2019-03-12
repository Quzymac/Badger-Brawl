﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour

    
{
    //This code quits the game
    public void QuitGame()
    {
        Debug.Log("closed");
        Application.Quit();
    }
     
    //This code will change from MainMenuScene to GameScene
    public void play()
    {
        SceneManager.LoadScene("GameScene");
    }

    //This code will change from GameScene to MainMenuScene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public bool PauseMenu = false;

    public GameObject PausePanel;

    //this method will enable the pausepanel in Unity, set the timescale to 0 so the game will pause and then turn the PauseMenu bool to true 
    void PauseGame ()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;

        PauseMenu = true;
    }

    //this method will disable the pausepanel in Unity, set the timescale to normal and then turn the PauseMenu bool to false
    public void Resume ()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;

        PauseMenu = false;
    }

  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ) // when you click on the Escape button this code will check if PauseMenu is true or false, if it is false then the pausemenu will open
        {
            if (!PauseMenu)
            {
                PauseGame();
            }


        }
    }
}
