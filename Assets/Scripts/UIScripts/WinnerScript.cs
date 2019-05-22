using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player {
    public class WinnerScript : MonoBehaviour
    {

        [SerializeField] GameObject HumansWin;  //winnerpanel for humans
        [SerializeField] GameObject BadgersWin;  // winnerpanel for bagders
        [SerializeField] GameObject buttons;  //buttons

        [SerializeField] float wait = 1f;

        public void PlayAgain() // code that will reload the current scene (in this case the GameScene)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu() // code that goes to MainMenuScene
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenuScene");
        }

        public void Win(PlayerScript.PlayerTeam winningTeam)
        {
            Time.timeScale = 0f;
            if (winningTeam == PlayerScript.PlayerTeam.human)
            {
                HumansWin.SetActive(true);
            }
            else if (winningTeam == PlayerScript.PlayerTeam.badger)
            {
                BadgersWin.SetActive(true);
            }
            StartCoroutine(DelayOnButtons());
        }

        IEnumerator DelayOnButtons()
        {
            yield return new WaitForSecondsRealtime(wait);
            buttons.SetActive(true);
        }
    }
}
