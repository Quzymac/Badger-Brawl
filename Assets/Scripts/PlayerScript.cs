using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerScript : MonoBehaviour
    {
        public enum PlayerTeam { none, human, badger }
        [SerializeField] public PlayerTeam Team { get; set; }

        GameManager gameManager;
        HealthBar healthBar;
        public float Health { get; set; } = 100f;
        public int playerNumber;
        [SerializeField] int joystick;
        [SerializeField] PlayerTeam t;
        public bool dead = false;

        private void Start()
        {
            t = Team;
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            healthBar = FindObjectOfType<HealthBarManager>().GetComponent<HealthBarManager>().healthBars[playerNumber - 1];
        }

        public static GameObject CreatePlayer(int playerNum, int joystickNum, PlayerTeam playerTeam, GameObject playerPrefab, Vector3 position)
        {
            GameObject player = Instantiate(playerPrefab, position, Quaternion.Euler(0, 90, 0));
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            playerScript.playerNumber = playerNum;
            playerScript.joystick = joystickNum;
            playerScript.Team = playerTeam;
            player.GetComponent<NewControllerInputs>().JoystickNumber = joystickNum;
    
        return player;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Death();
            }
            healthBar.UpdateHealthBar();
            Debug.Log("Player" + playerNumber + " - " + damage + " hp");
        }

        void Death()
        {
            dead = true;
            gameObject.GetComponent<NewControllerInputs>().DropWeapon();
            gameManager.TeamIsDead((PlayerTeam)playerNumber);

            Destroy(gameObject);

        }
    }
}