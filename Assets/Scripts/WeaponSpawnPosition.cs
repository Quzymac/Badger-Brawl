using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponSpawnPosition : MonoBehaviour //trigger för vapnet som kollar om det finns ett vapen där.
    {
        public bool occupied = false;
        GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Weapon" && occupied == false)
            {
                occupied = true;
            }
        }

        private void OnTriggerExit(Collider other) //fungerar inte och behövs förmodligen inte
        {
            if (other.gameObject.tag == "Weapon")
            {
                occupied = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Weapon")
            {
                Physics.IgnoreCollision(other, gameObject.GetComponent<Collider>());
            }
        }
        private void Update()
        {
            if (gameManager.GetComponent<WeaponSpawning>().newRound == true)
            {
                occupied = false;
            }
        }
    }
}
