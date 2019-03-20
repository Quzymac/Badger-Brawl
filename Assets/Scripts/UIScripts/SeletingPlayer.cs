using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeletingPlayer : MonoBehaviour
{
    bool playersAreReady = false;
    [SerializeField] GameObject playersReadyText;
   
    [SerializeField] int[] playerController = new int[4];


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
        if(Input.GetButtonDown("StartGame") && playersAreReady)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        if(!playersAreReady && PlayersReady() && AnyPlayerSet())
        {
            playersAreReady = true;
            playersReadyText.SetActive(true);
        }
        if (playersAreReady && !PlayersReady() && AnyPlayerSet())
        {
            playersAreReady = false;
            playersReadyText.SetActive(false);
        }
    }

    bool AnyPlayerSet()
    {
        for (int i = 0; i < 3; i++)
        {
            if(PlayerPrefs.GetInt("Player" + (i + 1).ToString()) > 0)
            {
                return true;
            }
        }
        return false;
    }

    bool PlayersReady()
    {
        for (int i = 0; i < selectTeam.Length; i++)
        {
            if (selectTeam[i].GetComponent<SelectTeamAndCharacter>().CurrentlySelecting)
            {
                return false;
            }
        }
        return true;
    }

    public void RemovePlayer(int joystick)
    {
        PlayerPrefs.SetInt("Player" + playerController[joystick - 1].ToString(), 0);
        PlayerPrefs.SetInt("Player" + playerController[joystick - 1].ToString() + "Joystick", 0);
        PlayerPrefs.SetInt("Player" + playerController[joystick - 1].ToString() + "Team", 0);
        PlayerPrefs.SetInt("Player" + playerController[joystick - 1].ToString() + "Character", 0);

        int temp = playerController[joystick - 1] - 1;
        playerController[joystick-1] = 0;
        
        PressToJoin[temp].enabled = true;
        selectTeam[temp].SetActive(false);
        selectTeam[temp].GetComponent<SelectTeamAndCharacter>().Controller = 0;
        selectTeam[temp].GetComponent<SelectTeamAndCharacter>().PlayerNum = 0;
        selectTeam[temp].GetComponent<SelectTeamAndCharacter>().CurrentlySelecting = false;
    }

    int SetPlayerNumber()
    {
        for (int i = 0; i < selectTeam.Length; i++)
        {
            if(selectTeam[i].GetComponent<SelectTeamAndCharacter>().PlayerNum == 0)
            {
                return i + 1;
            }
        }
        return 0;
    }

    void SelectPlayerController(int controller)
    {
        if (playerController[controller] == 0)
        {
            int player = SetPlayerNumber();

            playerController[controller] = player;
            PressToJoin[player - 1].enabled = false;
            selectTeam[player - 1].SetActive(true);
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().Controller = controller + 1;
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().PlayerNum = player;
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().CurrentlySelecting = true;
        }
    }
}
