using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectPlayer;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject options;
    

    //This code quits the game
    public void QuitGame()
    {
        Debug.Log("closed");
        Application.Quit();
    }
     
    //This code will change from MainMenuScene to GameScene
    //public void play()
    //{
    //    SceneManager.LoadScene("GameScene");
    //}

    public void SelectPlayers()
    {
        selectPlayer.SetActive(true);
        mainCanvas.SetActive(false);
    }

    

  

  
    // Update is called once per frame
    void Update()
    {
        
        if (credits.activeSelf == true && Input.GetButtonDown("BackController"))
        {
            credits.SetActive(false);
        }
        if (options.activeSelf == true && Input.GetButtonDown("BackController"))
        {
            options.SetActive(false);
        }
    }

    private void Start()
    {
        selectPlayer.SetActive(false);
    }
}
