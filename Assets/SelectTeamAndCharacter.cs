using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTeamAndCharacter : MonoBehaviour
{

    public int Controller { get; set; }
    public int PlayerNum { get; set; }
    int team;
    int characterNumber = -1;

    bool selecting = true;
    bool selectingTeam = true;

    [SerializeField] GameObject teams;
    [SerializeField] GameObject characters;
    [SerializeField] Button human;
    [SerializeField] Button badger;

    [SerializeField] Button[] character = new Button[4];

    float timer;

    public void SelectHuman()
    {
        team = 1;
    }
    public void SelectBadger()
    {
        team = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (selecting)
        {
            if (selectingTeam)
            {
                //move up
                if (Input.GetAxis("VerticalController" + Controller.ToString()) > 0.5f)
                {
                    human.Select();
                    team = 1;
                    timer = Time.time;
                }
                //move down
                if (Input.GetAxis("VerticalController" + Controller.ToString()) < -0.5f)
                {
                    badger.Select();
                    team = 2;
                    timer = Time.time;
                }
                if (Input.GetButtonDown("JumpController" + Controller.ToString()))
                {
                    selectingTeam = false;

                    teams.SetActive(false);
                    characters.SetActive(true);
                }
            }
            else
            {
                //move up
                if (Input.GetAxisRaw("VerticalController" + Controller.ToString()) > 0.5f && Time.time > 0.2f + timer)
                {
                    characterNumber--;
                    if (characterNumber <= 0)
                    {
                        characterNumber = 0;
                    }
                    character[characterNumber].Select();
                    timer = Time.time;

                }
                //move down
                if (Input.GetAxisRaw("VerticalController" + Controller.ToString()) < -0.5f && Time.time > 0.2f + timer)
                {
                    characterNumber++;
                    if (characterNumber >= 3)
                    {
                        characterNumber = 3;
                    }
                    character[characterNumber].Select();
                    timer = Time.time;

                }
                if (Input.GetButtonDown("JumpController" + Controller.ToString()))
                {
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString(), PlayerNum);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Joystick", Controller);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Team", team);
                    PlayerPrefs.SetInt("Player" + PlayerNum.ToString() + "Character", characterNumber);
                    Debug.Log(PlayerNum + " " + Controller + " " + team + " " + characterNumber);
                    Debug.Log(PlayerPrefs.GetInt("Player" + PlayerNum.ToString() + "Joystick") +" " + PlayerPrefs.GetInt("Player" + PlayerNum.ToString() + "Team") +" "+ PlayerPrefs.GetInt("Player" + PlayerNum.ToString() + "Character"));
                }
            }
        }
       
    }
}
