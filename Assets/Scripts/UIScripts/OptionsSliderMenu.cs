using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliderMenu : MonoBehaviour
{
    [SerializeField] UIAudioScript audioScript;
    [SerializeField] GameObject[] highlightedButtons;
    int currentButton = 0;
    float timer;
    bool canMove = true;
    [SerializeField] float sliderAdd = 2f;
    [SerializeField] Slider[] sliders;


    private void Start()
    {
        foreach (GameObject obj in highlightedButtons)
        {
            obj.gameObject.SetActive(false);
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
        if (Input.GetAxis("MenuVertical2") > 0.5f && Time.unscaledTime > 0.2f + timer) //move up
        {
            SelectButton(true);
        }
        if (Input.GetAxis("MenuVertical2") < -0.5f && Time.unscaledTime > 0.2f + timer) //move down
        {
            SelectButton(false);
        }
        if (Input.GetAxis("MenuHorizontal2") < -0.5f && Time.unscaledTime > 0.2f + timer && sliders[currentButton] != null) //move slider right/up
        {
            sliders[currentButton].GetComponent<Slider>().value += sliderAdd;
        }
        if (Input.GetAxis("MenuHorizontal2") > 0.5f && Time.unscaledTime > 0.2f + timer && sliders[currentButton] != null) //move slider left/down
        {
            sliders[currentButton].GetComponent<Slider>().value -= sliderAdd;
        }

        if (Input.GetButtonDown("PlayerJoiningGame"))
        {
            if (highlightedButtons[currentButton].GetComponent<Button>() != null)
            {
                highlightedButtons[currentButton].GetComponent<Button>().onClick.Invoke(); //click button
            }
        }
    }
}
