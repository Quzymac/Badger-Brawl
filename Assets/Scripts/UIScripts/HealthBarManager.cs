using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthBarManager : MonoBehaviour
    {
        public List<PlayerScript> Players = new List<PlayerScript>(); //spelarna som ska få rätt health bar
        public HealthBar[] healthBars = new HealthBar[4];
      
        public void NewRound()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                healthBars[i].SpecificPlayer = Players[i];
            }
            for (int i = 0; i < healthBars.Length; i++)
            {
                healthBars[i].SetHealthbarsActive();
            }
        }
    }
}
