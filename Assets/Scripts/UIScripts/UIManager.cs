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
    [SerializeField] GameObject quit;
    [SerializeField] GameObject howToPlay;
    
    float buttonTimer = 0.1f;
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void OpenCanvas(GameObject canvas)
    {
        StartCoroutine(OpenTimer(canvas));
    }
    IEnumerator OpenTimer(GameObject canvas)
    {
        yield return new WaitForSecondsRealtime(buttonTimer);
        canvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void BackToMain()
    {
        StartCoroutine(BackToMainTimer());
    }
    IEnumerator BackToMainTimer()
    {
        yield return new WaitForSecondsRealtime(buttonTimer);
        mainCanvas.SetActive(true);
        selectPlayer.SetActive(false);
        credits.SetActive(false);
        options.SetActive(false);
        quit.SetActive(false);
        howToPlay.SetActive(false); 
    } 
  
    // Update is called once per frame
    void Update()
    {
        if ((credits.activeInHierarchy || options.activeInHierarchy || quit.activeInHierarchy || howToPlay.activeInHierarchy) && Input.GetButtonDown("BackController"))
        {
            BackToMain();
        }
    }

    private void Start()
    {
        selectPlayer.SetActive(false);
    }
}
