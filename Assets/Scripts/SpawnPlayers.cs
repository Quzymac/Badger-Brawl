using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SpawnPlayers : MonoBehaviour
    {
        GameObject[,] characters = new GameObject[2, 2];
        [SerializeField]GameObject player;

        [SerializeField] int[] playerNum = new int[4];
        [SerializeField] int[] playerController = new int[4];
        [SerializeField] int[] playerTeam = new int[4];
        [SerializeField] int[] playerCharacter = new int[4];


        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                playerNum[i] = PlayerPrefs.GetInt("Player" + (i+1).ToString());
                playerController[i] = PlayerPrefs.GetInt("Player" + (i+1).ToString() + "Joystick");
                playerTeam[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Team");
                playerCharacter[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Character");
                if(playerController[i] != 0)
                {
                    GameObject players = PlayerScript.CreatePlayer(playerNum[i], playerController[i], (PlayerScript.PlayerTeam)playerTeam[i], player, new Vector3(0, 0, 0));
                }
            }

        }
    }
}

