﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Transform bar; // själva health baren
        public PlayerScript SpecificPlayer { get; set; }
        float maxHealth;

        void Start()
        {
            if (SpecificPlayer == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                maxHealth = SpecificPlayer.GetComponent<PlayerScript>().Health;
            }
        }

        void Update()
        {
            
        }

        public void UpdateHealthBar()
        {
            bar.localScale = new Vector3(SpecificPlayer.Health / maxHealth, 1f);

            Debug.Log(SpecificPlayer.playerNumber + " Hit");
        }
    }
}
