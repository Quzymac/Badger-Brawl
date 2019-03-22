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
    bool canMove = true;

    void Awake()
    {
        buttonsAnimator = new Animator[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonsAnimator[i] = buttons[i].GetComponent<Animator>();
        }
        currentButton = 0;
    }
    private void Start()
    {
        buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.highlightedTrigger);
    }

    void SelectButton(int button, bool moveUp)
    {
        buttonsAnimator[button].SetTrigger(buttons[button].animationTriggers.normalTrigger);

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
            if (currentButton >= (buttons.Length - 1))
            {
                currentButton = (buttons.Length - 1);
            }
        }
        buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.highlightedTrigger);

        timer = Time.unscaledTime;
    }
    private void OnEnable() //when canvas is enabled
    {
        currentButton = 0;
        canMove = true;
        buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.highlightedTrigger);
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxis("MenuVertical2") > 0.5f && Time.unscaledTime > 0.2f + timer) //move up
            {
                SelectButton(currentButton, true);
            }
            if (Input.GetAxis("MenuVertical2") < -0.5f && Time.unscaledTime > 0.2f + timer) //move down
            {
                SelectButton(currentButton, false);
            }
            if (Input.GetButtonDown("PlayerJoiningGame")) //click button
            {
                StartCoroutine(ClickButton(currentButton));
            }
        }
        IEnumerator ClickButton(int button)
        {
            buttonsAnimator[currentButton].SetTrigger(buttons[currentButton].animationTriggers.normalTrigger);
            canMove = false;
            yield return new WaitForSecondsRealtime(0.15f); //timer to let button animation finish
            buttons[button].onClick.Invoke();

        }
    }
}
