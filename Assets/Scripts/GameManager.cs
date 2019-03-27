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

        void Start()
        {
            Debug.Log(Badgers.Count + " " + Humans.Count);
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
                return true;
            }
        }
    }
}
