using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{
    Animator[] buttonsAnimator;
    [SerializeField] Button[] buttons;
    int currentButton;
    float timer;

    void Start()
    {
        buttonsAnimator = new Animator[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsAnimator[i] = buttons[i].GetComponent<Animator>();
        }
        currentButton = 0;
        SelectButton(currentButton);
    }

    void SelectButton(int button)
    {
        buttons[button].Select();

        buttonsAnimator[button].SetTrigger(buttons[button].animationTriggers.highlightedTrigger);
    }

    void Update()
    {
        if (Input.GetAxis("MenuVertical2") > 0.5f && Time.unscaledTime > 0.2f + timer) //move up
        {
            buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.normalTrigger);

            currentButton--;
            if (currentButton <= 0)
            {
                currentButton = 0;
            }
            SelectButton(currentButton);

            timer = Time.unscaledTime;
        }
        if (Input.GetAxis("MenuVertical2") < -0.5f && Time.unscaledTime > 0.2f + timer) //move down
        {
            buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.normalTrigger);

            currentButton++;
            if (currentButton >= (buttons.Length - 1))
            {
                currentButton = (buttons.Length - 1);
            }
            SelectButton(currentButton);

            timer = Time.unscaledTime;
        }
        if (Input.GetButtonDown("PlayerJoiningGame")) //click
        {
            buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.normalTrigger);
            buttons[currentButton].onClick.Invoke();
        }
    }
}
