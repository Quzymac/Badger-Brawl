using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Transform bar; // själva health baren
        [SerializeField] public SpriteRenderer image;

        SpawnPlayers spawnPlayers;
        [SerializeField] int player;
        public List<Sprite> BadgerFace = new List<Sprite>();
        public List<Sprite> HumanFace = new List<Sprite>();
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
                SetHealthBarCharacter();
            }
        }

        public void UpdateHealthBar()
        {
            bar.localScale = new Vector3((SpecificPlayer.Health / maxHealth) * 0.59f , 0.6f);

            Debug.Log(SpecificPlayer.playerNumber + " Hit");
        }

        public void SetHealthBarCharacter() //sätter spriten av ansiktet för respektive karaktär på healtBar
        {
            if (PlayerPrefs.GetInt("Player" + player + "Team") == 1)
            {
                image.sprite = HumanFace[PlayerPrefs.GetInt("Player" + player + "Character")];
            }
            else if(PlayerPrefs.GetInt("Player" + player + "Team") == 2)
            {
                image.sprite = BadgerFace[PlayerPrefs.GetInt("Player" + player + "Character")];
            }
        }
    }
}
