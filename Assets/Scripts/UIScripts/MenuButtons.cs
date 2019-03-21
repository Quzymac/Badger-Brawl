using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuButtons : MonoBehaviour
{

    [SerializeField] Button[] buttons = new Button[4];
    int currentButton;
    float timer;

    void Start()
    {
        currentButton = 0;
        buttons[0].Select();
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
            buttons[currentButton].Select();
            timer = Time.time;
        }
        if (Input.GetAxis("MenuVertical2") < -0.5f && Time.time > 0.2f + timer) //move down
        {
            currentButton++;
            if (currentButton >= 3)
            {
                currentButton = 3;
            }
            buttons[currentButton].Select();
            timer = Time.time;
        }
        if (Input.GetButtonDown("PlayerJoiningGame")) //click
        {
            buttons[currentButton].onClick.Invoke();
        }
    }
}
