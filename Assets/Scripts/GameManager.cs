using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {

    public class GameManager : MonoBehaviour
    {
        List<PlayerScript> badgers = new List<PlayerScript>(); //hålla koll på badgers
        public List<PlayerScript> Badgers { get { return badgers; } set { badgers = value; } } //hålla koll på badgers

        List<PlayerScript> humans = new List<PlayerScript>(); //hålla koll på humans
        public List<PlayerScript> Humans { get { return humans; } set { humans = value; } } //hålla koll på humans

        CAM_CamerMovement cameraMovement;
        float cameraValue = 20f;

        int winningPlayer;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                foreach (var badger in badgers)
                {
                    if(badger != null)
                    {
                       Destroy(badger.gameObject);
                    }
                }
                foreach (var human in humans)
                {
                    if(human != null)
                    {
                        Destroy(human.gameObject);
                    }
                }
                gameObject.GetComponent<SpawnPlayers>().Spawn();
            }
        }

        void Start()
        {           
            cameraMovement = FindObjectOfType<CAM_CamerMovement>();
        }


        public bool TeamIsDead(PlayerScript.PlayerTeam team)
        {
            if (team == PlayerScript.PlayerTeam.human)
            {
                for (int i = 0; i < humans.Count; i++)
                {
                    if (humans[i].dead == false)
                    {
                        return false;
                    }
                }
                NewRound(PlayerScript.PlayerTeam.badger);
                return true;
            }
            else
            {
                for (int i = 0; i < badgers.Count; i++)
                {
                    if (badgers[i].dead == false)
                    {
                        return false;
                    }
                }
                NewRound(PlayerScript.PlayerTeam.human);
                return true;
            }
        }


        public void NewRound(PlayerScript.PlayerTeam winner)
        {
            if (winner == PlayerScript.PlayerTeam.badger)
            {
                cameraMovement.ChangeCameraPos(cameraValue);
            }
            else if (winner == PlayerScript.PlayerTeam.human)
            {
                cameraMovement.ChangeCameraPos(-cameraValue);
            }

            foreach (var badger in badgers)
            {
                if (badger != null)
                {
                    Destroy(badger.gameObject);
                }
            }
            foreach (var human in humans)
            {
                if (human != null)
                {
                    Destroy(human.gameObject);
                }
            }
            gameObject.GetComponent<SpawnPlayers>().Spawn();
        }
    }
}
