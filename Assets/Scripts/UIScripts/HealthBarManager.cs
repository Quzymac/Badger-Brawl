using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] GameManager gameManager;
        public List<PlayerScript> Players = new List<PlayerScript>(); //spelarna som ska få rätt health bar
        [SerializeField] HealthBar[] healthBars = new HealthBar[4];

        void Start()
        {
            Debug.Log(Players.Count);

            for (int i = 0; i < Players.Count; i++)
            {

                healthBars[i].SpecificPlayer = Players[i];
                healthBars[i].SetSize(0.5f);
                Debug.Log(healthBars[i].SpecificPlayer);
            }
        }

        void Update()
        {
            
        }
    }
}
