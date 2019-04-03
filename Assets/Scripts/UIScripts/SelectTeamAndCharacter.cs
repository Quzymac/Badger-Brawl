using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeamAndCharacter : MonoBehaviour
{

    public int Controller { get; set; } = 0;
    public int PlayerNum { get; set; } = 0;
    int team = 1;
    int characterNumber = 0;

    public bool CurrentlySelecting { get; set; } = false;
    bool selectingTeam = true;
    bool selectingCharacter = false;

    [SerializeField] SeletingPlayer playerSelectScript;

    [SerializeField] Text readyText;
    [SerializeField] GameObject teams;
    [SerializeField] GameObject characters;
    [SerializeField] Button human;
    [SerializeField] Button badger;

    [SerializeField] Button[] character = new Button[4];

    Color buttonDefaultColor = Color.white;
    [SerializeField] Color buttonColor;

    float timer;

    private void Start()
    {
        SetButtonColor(human, buttonColor);
        SetButtonColor(character[0], buttonColor);

        int c = 0;
        string[] joy = Input.GetJoystickNames();
        foreach (var item in joy)
        {
            c++;
            Debug.Log(c + " " + item);
        }
    }

    void SetButtonColor(Button button, Color color)
    {
        ColorBlock c = button.colors;
        c.normalColor = color;
        button.colors = c;
    }

    // Update is called once per frame
    void Update()
    {
        //back
        if (Input.GetButtonDown("DropWeapon" + Controller.ToString()))
        {
            if(CurrentlySelecting && selectingTeam)
            {
                playerSelectScript.RemovePlayer(Controller);
            }
            if(CurrentlySelecting && selectingCharacter)
            {
                selectingCharacter = false;
                selectingTeam = true;
                playerSelectScript.playerDone[PlayerNum - 1] = false;
            }
            if (!CurrentlySelecting && selectingCharacter)
            {
                CurrentlySelecting = true;
            }
        }
        if (CurrentlySelecting)
        {
            if(readyText.text != "Player " + PlayerNum)
            {
                readyText.text = "Player " + PlayerNum;
            }

            if (selectingTeam) //select team
            {
                if (!teams.activeInHierarchy)
                {
                    teams.SetActive(true);
                }
                if (characters.activeInHierarchy)
                {
                    characters.SetActive(false);
                }
                //move up
                if (Input.GetAxis("VerticalController" + Controller.ToString()) > 0.5f)
                {
                    SetButtonColor(badger, buttonDefaultColor);
                    SetButtonColor(human, buttonColor);

                    team = 1;
                    timer = Time.time;
                }
                //move down
                if (Input.GetAxis("VerticalController" + Controller.ToString()) < -0.5f)
                {
                    SetButtonColor(human, buttonDefaultColor);
                    SetButtonColor(badger, buttonColor);

                    team = 2;
                    timer = Time.time;
                }
                if (Input.GetButtonDown("JumpController" + Controller.ToString()))
                {
                    selectingTeam = false;
                    selectingCharacter = true;

                    teams.SetActive(false);
                    characters.SetActive(true);
                }
            }
            else if(!selectingTeam && selectingCharacter)// select character
            {

                if (teams.activeInHierarchy)
                {
                    teams.SetActive(false);
                }
                if (!characters.activeInHierarchy)
                {
                    characters.SetActive(true);
                }
                //move up
                if (Input.GetAxisRaw("VerticalController" + Controller.ToString()) > 0.5f && Time.time > 0.2f + timer)
                {
                    if (characterNumber >= 0)
                    {
                        SetButtonColor(character[characterNumber], buttonDefaultColor);
                    }

                    characterNumber--;
                    if (characterNumber <= 0)
                    {
                        characterNumber = 0;
                    }
                    SetButtonColor(character[characterNumber], buttonColor);

                    timer = Time.time;

                }
                //move down
                if (Input.GetAxisRaw("VerticalController" + Controller.ToString()) < -0.5f && Time.time > 0.2f + timer)
                {
                    if (characterNumber >= 0)
                    {
                        SetButtonColor(character[characterNumber], buttonDefaultColor);
                    }
                    characterNumber++;
                    if (characterNumber >= 3)
                    {
                        characterNumber = 3;
                    }
                    SetButtonColor(character[characterNumber], buttonColor);

                    timer = Time.time;

                }
                //confirm selected character
                if (Input.GetButtonDown("JumpController" + Controller.ToString()))
                {
                    readyText.text = "Player " + PlayerNum + " ready!";
                    playerSelectScript.playerDone[PlayerNum - 1] = true;
                    CurrentlySelecting = false;
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString(), PlayerNum);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Joystick", Controller);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Team", team);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Character", characterNumber);
                    // show ready icon here
                }
                
            }
        }
       
    }
}
