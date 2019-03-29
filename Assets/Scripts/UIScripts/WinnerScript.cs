using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerScript : MonoBehaviour
{
    
    [SerializeField] GameObject HumansWin;  //winnerpanel for humans
    [SerializeField] GameObject BadgersWin;  // winnerpanel for bagders

    public void PlayAgain() // code that will reload the current scene (in this case the GameScene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() // code that goes to MainMenuScene
    {
        SceneManager.LoadScene("MainMenuScene");

    }

    public void humansWon() // method to stop the time and enable the humans win panel
    {
        Time.timeScale = 0f;
        HumansWin.SetActive(true);
    }

    public void badgersWon() // method to stop the time and enable the badgers win panel
    {
        Time.timeScale = 0f;
        BadgersWin.SetActive(true);

    }
  
}
