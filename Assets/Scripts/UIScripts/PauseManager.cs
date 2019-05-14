using System.Collections;
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
    public GameObject HowToPlayPanel;

    float buttonTimer = 0.1f;


   

    //this method will enable the pausepanel in Unity, set the timescale to 0 so the game will pause and then turn the PauseMenu bool to true 
    void PauseGame()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);

        PauseMenu = true;

    }

    //this method will disable the pausepanel in Unity, set the timescale to normal and then turn the PauseMenu bool to false
    public void Resume()
    {
        PausePanel.SetActive(false);
        GetComponent<CountDownPause>().countdownCanvas.SetActive(true);
        PauseMenu = false;
    }
    public void OpenCanvas(GameObject canvas)
    {
        StartCoroutine(OpenTimer(canvas));
    }
    IEnumerator OpenTimer(GameObject canvas)
    {
        yield return new WaitForSecondsRealtime(buttonTimer);
        canvas.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        PausePanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        ExitToMainMenuPanel.SetActive(false);
    }

    //this method will make you go back to the pausemenu 
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void HowToPlayChosen()
    {
        HowToPlayPanel.SetActive(true);
        PausePanel.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("StartGame"))) // when you click on the Escape button this code will check if PauseMenu is true or false, if it is false then the pausemenu will open
        {
            if (!PauseMenu)
            {
                GetComponent<CountDownPause>().countdownCanvas.SetActive(false);
                GetComponent<CountDownPause>().count = -1; //resets timer
                PauseGame();
            }
        }
    }

}
