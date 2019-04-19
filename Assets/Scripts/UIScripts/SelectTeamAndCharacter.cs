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
    [SerializeField] Sprite humanImage;
    [SerializeField] Sprite humanImage1;
    [SerializeField] Sprite badgerImage;
    [SerializeField] Sprite badgerImage1;
    public bool CurrentlySelecting { get; set; } = false;
    bool selectingTeam = true;
    bool selectingCharacter = false;

    [SerializeField] SeletingPlayer playerSelectScript;

    [SerializeField] Text readyText;
    [SerializeField] GameObject teams;
    [SerializeField] GameObject characters;
    [SerializeField] GameObject human;
    [SerializeField] GameObject badger;

    [SerializeField] Image[] character = new Image[2];
    [SerializeField] Image[] highlight = new Image[2];

    Color buttonDefaultColor = Color.white;
    [SerializeField] Color buttonColor;

    float timer;

    private void Start()
    {
        //SetButtonColor(human, buttonColor);
        for (int i = 0; i < highlight.Length; i++)
        {
            highlight[i].color = buttonColor;
            highlight[i].enabled = false;
        }
        highlight[0].enabled = true;


        int c = 0;
        string[] joy = Input.GetJoystickNames();
        foreach (var item in joy)
        {
            c++;
            Debug.Log(c + " " + item);
        }
    }

    void SetButtonColor(Image button, Color col)
    {
        button.color = col;
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
            if(readyText.enabled)
            {
                readyText.enabled = false;
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
                    badger.SetActive(false);
                    human.SetActive(true);
                   // SetButtonColor(badger, buttonDefaultColor);
                   // SetButtonColor(human, buttonColor);

                    team = 1;
                    character[0].sprite = humanImage;
                    character[1].sprite = humanImage1;

                    //character[1].image.sprite = humanImage1;
                    timer = Time.time;
                }
                //move down
                if (Input.GetAxis("VerticalController" + Controller.ToString()) < -0.5f)
                {
                    human.SetActive(false);
                    badger.SetActive(true);
                    //SetButtonColor(human, buttonDefaultColor);
                    //SetButtonColor(badger, buttonColor);

                    team = 2;
                    character[0].sprite = badgerImage;
                    character[1].sprite = badgerImage1;
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
                        highlight[characterNumber].enabled = false;
                    }

                    characterNumber--;
                    if (characterNumber <= 0)
                    {
                        characterNumber = 0;
                    }
                    highlight[characterNumber].enabled = true;

                    timer = Time.time;

                }
                //move down
                if (Input.GetAxisRaw("VerticalController" + Controller.ToString()) < -0.5f && Time.time > 0.2f + timer)
                {
                    if (characterNumber >= 0)
                    {
                        highlight[characterNumber].enabled = false;
                    }
                    characterNumber++;
                    if (characterNumber >= 1)
                    {
                        characterNumber = 1;
                    }
                    highlight[characterNumber].enabled = true;

                    timer = Time.time;

                }
                //confirm selected character
                if (Input.GetButtonDown("JumpController" + Controller.ToString()))
                {
                    readyText.enabled = true;
                    playerSelectScript.playerDone[PlayerNum - 1] = true;
                    CurrentlySelecting = false;
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString(), PlayerNum);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Joystick", Controller);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Team", team);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Character", characterNumber);
                }
                
            }
        }
       
    }
}
