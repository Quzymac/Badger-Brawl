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
    [SerializeField] Text[] HasJoined = new Text[4];

    [SerializeField] Button[] teamButtons = new Button[8];
    [SerializeField] Button[] characterButtons = new Button[8];


    // button.onClick.Invoke(); för att clicka på knappar i script


    public void SetPlayerPrefs()
    {
        for (int i = 0; i < playerController.Length; i++)
        {
            PlayerPrefs.SetInt("playerController" + i, playerController[i]);
            PlayerPrefs.SetInt("playerTeam" + i, playerTeam[i]);
            PlayerPrefs.SetInt("playerCharacter" + i, playerCharacter[i]);
        }
    }

    void Start()
    {
        for (int i = 0; i < PressToJoin.Length; i++)
        {
            PressToJoin[i].enabled = true;
        }
        for (int j = 0; j < HasJoined.Length; j++)
        {
            HasJoined[j].enabled = false;
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
            HasJoined[playerChoosing - 1].enabled = true;
            playerChoosing++;
        }
    }
}
