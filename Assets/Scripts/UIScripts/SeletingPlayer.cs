using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeletingPlayer : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    public bool[] playerDone = new bool[4];
    bool playersAreReady = false;
    [SerializeField] GameObject playersReadyText;

    [SerializeField] int[] playerController = new int[16];


    [SerializeField] Image[] PressToJoin = new Image[4];
    [SerializeField] Text[] PressToJoinText = new Text[4];

    [SerializeField] GameObject[] selectTeam = new GameObject[4];

    void Start()
    {
        for (int i = 0; i < playerDone.Length; i++)
        {
            playerDone[i] = false;

            PlayerPrefs.SetInt("Player" + i.ToString(), 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Joystick", 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Team", 0);
            PlayerPrefs.SetInt("Player" + i.ToString() + "Character", 0);
        }

        for (int i = 0; i < PressToJoin.Length; i++)
        {
            PressToJoin[i].enabled = false;
            PressToJoinText[i].enabled = true;
        }
        for (int j = 0; j < selectTeam.Length; j++)
        {
            selectTeam[j].SetActive(false);
        }
        PlayerPrefs.DeleteAll();

    }

    void Update()
    {
        if (Input.GetButtonDown("BackController") && !AnyPlayerDone() && PlayersReady()) 
        {
            uiManager.BackToMain();
        }
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
            else if (Input.GetButtonDown("JumpController5"))
            {
                SelectPlayerController(4);
            }
            else if (Input.GetButtonDown("JumpController6"))
            {
                SelectPlayerController(5);
            }
            else if (Input.GetButtonDown("JumpController7"))
            {
                SelectPlayerController(6);
            }
            else if (Input.GetButtonDown("JumpController8"))
            {
                SelectPlayerController(7);
            }
            else if (Input.GetButtonDown("JumpController9"))
            {
                SelectPlayerController(8);
            }
            else if (Input.GetButtonDown("JumpController10"))
            {
                SelectPlayerController(9);
            }
            else if (Input.GetButtonDown("JumpController11"))
            {
                SelectPlayerController(10);
            }
            else if (Input.GetButtonDown("JumpController12"))
            {
                SelectPlayerController(11);
            }
            else if (Input.GetButtonDown("JumpController13"))
            {
                SelectPlayerController(12);
            }
            else if (Input.GetButtonDown("JumpController14"))
            {
                SelectPlayerController(13);
            }
            else if (Input.GetButtonDown("JumpController15"))
            {
                SelectPlayerController(14);
            }
            else if (Input.GetButtonDown("JumpController16"))
            {
                SelectPlayerController(15);
            }
        }
        if(Input.GetButtonDown("StartGame") && playersAreReady)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        if(!playersAreReady && PlayersReady() && AnyPlayerDone())
        {
            playersAreReady = true;
            playersReadyText.SetActive(true);
        }
        if (playersAreReady && !PlayersReady() && AnyPlayerDone())
        {
            playersAreReady = false;
            playersReadyText.SetActive(false);
        }
    }

    bool AnyPlayerDone()
    {
        for (int i = 0; i < 3; i++)
        {
            if(playerDone[i])
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
        
        PressToJoin[temp].enabled = false;
        PressToJoinText[temp].enabled = true;

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
            PressToJoin[player - 1].enabled = true;
            PressToJoinText[player - 1].enabled = false;

            selectTeam[player - 1].SetActive(true);
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().Controller = controller + 1;
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().PlayerNum = player;
            selectTeam[player - 1].GetComponent<SelectTeamAndCharacter>().CurrentlySelecting = true;
        }
    }
}
