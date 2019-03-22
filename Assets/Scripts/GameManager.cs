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


        void Update()
        {

        }
    }
}
