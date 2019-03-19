using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeletingPlayer : MonoBehaviour
{

    [SerializeField]int playerChoosing = 1;
   
    [SerializeField] int[] playerController = new int[4];
    [SerializeField] int[] playerTeam = new int[4];
    [SerializeField] int[] playerCharacter = new int[4];


    [SerializeField] Text[] PressToJoin = new Text[4];
    [SerializeField] GameObject[] selectTeam = new GameObject[4];


    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("Player" + i.ToString(), 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Joystick", 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Team", 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Character", 0);
        }

        for (int i = 0; i < PressToJoin.Length; i++)
        {
            PressToJoin[i].enabled = true;
        }
        for (int j = 0; j < selectTeam.Length; j++)
        {
            selectTeam[j].SetActive(false);// = false;
        }
        Debug.Log(Input.GetJoystickNames());
    }

    void Update()
    {
        if (Input.GetButtonDown("PlayerJoiningGame"))
        {
            if (Input.GetButtonDown("JumpController1"))
            {
                SelectPlayerController(0);
            }
            else if (Input.GetButtonDown("JumpController2"))
            {
                SelectPlayerController(1);
            }
            else if (Input.GetButtonDown("JumpController3"))
            {
                SelectPlayerController(2);
            }
            else if (Input.GetButtonDown("JumpController4"))
            {
                SelectPlayerController(3);
            }
        }
    }

    void SelectPlayerController(int controller)
    {
        if (playerController[controller] == 0)
        {
            playerController[controller] = playerChoosing;
            PressToJoin[playerChoosing - 1].enabled = false;
            selectTeam[playerChoosing - 1].SetActive(true);// enabled = true;
            selectTeam[playerChoosing - 1].GetComponent<SelectTeamAndCharacter>().Controller = controller + 1;
            selectTeam[playerChoosing - 1].GetComponent<SelectTeamAndCharacter>().PlayerNum = playerChoosing;
            playerChoosing++;
        }
    }
}
