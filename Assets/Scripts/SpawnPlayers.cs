using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SpawnPlayers : MonoBehaviour
    {
        [SerializeField] GameManager gameManager;
        [SerializeField] GameObject[] humanCharacters = new GameObject[2];
        [SerializeField] GameObject[] badgerCharacters = new GameObject[2];
        [SerializeField] MultipleTargetCam addTargets;
        //[SerializeField] GameObject player;
        //[SerializeField] GameObject badger;

        [SerializeField] int[] playerNum = new int[4];
        [SerializeField] int[] playerController = new int[4];
        [SerializeField] int[] playerTeam = new int[4];
        [SerializeField] int[] playerCharacter = new int[4];

        [SerializeField] List<Transform> spawnPositions = new List<Transform>();
        List<Transform> usedSpawnPositions = new List<Transform>();
        [SerializeField] Transform upperCollider;
        [SerializeField] Transform bottomCollider;

     
        public void Spawn()
        {
   
            ResetPlayers();
            for (int i = 0; i < 4; i++)
            {
                if (playerController[i] != 0)
                {
                    GameObject[] teamCharacter = new GameObject[2];
                    if(playerTeam[i] == (int)PlayerScript.PlayerTeam.human)
                    {
                        teamCharacter = humanCharacters;
                    }
                    else
                    {
                        teamCharacter = badgerCharacters;
                    }
                    GameObject players = PlayerScript.CreatePlayer(playerNum[i], playerController[i], (PlayerScript.PlayerTeam)playerTeam[i], teamCharacter[playerCharacter[i]], CheckSpawnPosition().position);

                    if (playerTeam[i] == 1)
                    {
                        gameManager.Humans.Add(players.GetComponent<PlayerScript>());
                    }
                    else if (playerTeam[i] == 2)
                    {
                        gameManager.Badgers.Add(players.GetComponent<PlayerScript>());
                    }
                    gameManager.GetComponent<HealthBarManager>().Players.Add(players.GetComponent<PlayerScript>());
                    addTargets.targets.Add(players.transform);
                }
            }
            gameManager.GetComponent<HealthBarManager>().NewRound();
        }
        void ResetPlayers()
        {
            addTargets.targets.Clear();
            usedSpawnPositions.Clear();
            for (int i = 0; i < 4; i++)
            {
                gameManager.GetComponent<HealthBarManager>().Players.Clear();
                gameManager.Badgers.Clear();
                gameManager.Humans.Clear();
            }
        }

        void Awake()
        {
            for (int i = 0; i < 4; i++)
            {
                playerNum[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString());
                playerController[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Joystick");
                playerTeam[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Team");
                playerCharacter[i] = PlayerPrefs.GetInt("Player" + (i + 1).ToString() + "Character");
            }
            Spawn();
        }

        Transform CheckSpawnPosition()
        {
            while (true)
            {
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                if (spawnPositions[randomSpawn].position.y > bottomCollider.position.y && spawnPositions[randomSpawn].position.y < upperCollider.position.y - 5 && !usedSpawnPositions.Contains(spawnPositions[randomSpawn]))
                {
                    usedSpawnPositions.Add(spawnPositions[randomSpawn]);
                    return spawnPositions[randomSpawn];
                }
            }
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log(upperCollider.position.y + " " + bottomCollider.position.y);
            }
        }
    }
}

