using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponSpawnPosition : MonoBehaviour //trigger för vapnet som kollar om det finns ett vapen där.
    {
        public bool occupied = false;
        GameManager gameManager;
        float timer;

        IWeapon weapon;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (occupied == false)
            {
                weapon = other.GetComponent<IWeapon>();
                timer = other.GetComponent<WeaponDespawn>().GetTimer();
                occupied = true;
            }
        }
        
        private void Update()
        {
           if (occupied)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    occupied = false;
                }
                else if (weapon != null && weapon.Owner != null)
                {
                    occupied = false;
                }
            }
        }
    }
}
