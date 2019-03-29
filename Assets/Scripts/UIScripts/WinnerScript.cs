using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerScript : MonoBehaviour
{
    [SerializeField] Canvas WinnerCanvas;
    [SerializeField] GameObject HumansWin;  //winnerpanel for humans
    [SerializeField] GameObject BadgersWin;  // winnerpanel for bagders
    
    bool humansWon = false;
    bool badgersWon = false;
    bool winnerScreen = false;


    public void PlayAgain() // code that will reload the current scene (in this case the GameScene)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() // code that goes to MainMenuScene
    {
        SceneManager.LoadScene("MainMenuScene");

    }


    // Update is called once per frame
    void Update()
    {
       while (winnerScreen)
        {
            
            Time.timeScale = 0f;

            if (humansWon)
            {
                HumansWin.SetActive(true);
            }
            if (badgersWon)
            {
                BadgersWin.SetActive(true);
            }
            
        }
    }
}
