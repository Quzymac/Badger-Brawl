using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Transform bar; // själva health baren
        [SerializeField] public SpriteRenderer badger;
        [SerializeField] public SpriteRenderer human;
        public PlayerScript SpecificPlayer { get; set; }
        float maxHealth;
        
        public void SetHealthbarsActive()
        {
            if (SpecificPlayer == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                maxHealth = SpecificPlayer.GetComponent<PlayerScript>().Health;
                UpdateHealthBar();
            }
        }

        public void UpdateHealthBar()
        {
            bar.localScale = new Vector3((SpecificPlayer.Health / maxHealth) * 0.59f , 0.6f);

            Debug.Log(SpecificPlayer.playerNumber + " Hit");
        }
    }
}
