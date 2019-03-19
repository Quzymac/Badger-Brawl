using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class PlayerScript : MonoBehaviour
    {
        public enum PlayerTeam { none, human, badger }
        [SerializeField] public PlayerTeam Team { get; set; }

        public float Health { get; set; } = 100f;
        [SerializeField] int playerNumber;
        [SerializeField] int joystick;


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
            Debug.Log("Player" + playerNumber + " - " + damage + " hp");
        }

    }
}