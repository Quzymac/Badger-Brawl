using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonsInMenu : MonoBehaviour
{
    [SerializeField] UIAudioScript audioScript;
    [SerializeField] Button[] highlightedButtons;
    int currentButton = 0;
    float timer;
    bool canMove = true;
    [SerializeField] bool upDownMovement = true;

    
    private void Start()
    {
        foreach (Button button in highlightedButtons)
        {
            button.gameObject.SetActive(false);
        }
        highlightedButtons[currentButton].gameObject.SetActive(true);
    }

    void SelectButton(bool moveUp)
    {
        if (highlightedButtons.Length > 1)
        {
            highlightedButtons[currentButton].gameObject.SetActive(false);
            audioScript.uiSounds.PlayOneShot(audioScript.hoverSound);

            if (moveUp)
            {
                currentButton--;
                if (currentButton <= 0)
                {
                    currentButton = 0;
                }
            }
            else
            {
                currentButton++;
                if (currentButton >= (highlightedButtons.Length - 1))
                {
                    currentButton = (highlightedButtons.Length - 1);
                }
            }
            highlightedButtons[currentButton].gameObject.SetActive(true);

            timer = Time.unscaledTime;
        }
    }

    void Update()
    {
        if (Input.GetAxis("MenuVertical2") > 0.5f && upDownMovement && Time.unscaledTime > 0.2f + timer) //move up
        {
            SelectButton(true);
        }
        if (Input.GetAxis("MenuVertical2") < -0.5f && upDownMovement && Time.unscaledTime > 0.2f + timer) //move down
        {
            SelectButton(false);
        }

        if (Input.GetAxis("MenuHorizontal2") > 0.5f && !upDownMovement && Time.unscaledTime > 0.2f + timer) //move left
        {
            SelectButton(true);
        }
        if (Input.GetAxis("MenuHorizontal2") < -0.5f && !upDownMovement && Time.unscaledTime > 0.2f + timer) //move right
        {
            SelectButton(false);
        }

        if (Input.GetButtonDown("PlayerJoiningGame")) 
        {
            highlightedButtons[currentButton].onClick.Invoke(); //click button
        }
    }
}
