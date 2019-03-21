﻿using System.Collections;
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
        if (Input.GetAxis("MenuVertical2") > 0.5f && Time.time > 0.2f + timer) //move up
        {
            currentButton--;
            if (currentButton <= 0)
            {
                currentButton = 0;
            }
            SelectButton(currentButton);

            timer = Time.time;
        }
        if (Input.GetAxis("MenuVertical2") < -0.5f && Time.time > 0.2f + timer) //move down
        {
            currentButton++;
            if (currentButton >= 3)
            {
                currentButton = 3;
            }
            SelectButton(currentButton);

            timer = Time.time;
        }
        if (Input.GetButtonDown("PlayerJoiningGame")) //click
        {
            buttons[currentButton].onClick.Invoke();
        }
    }
}
