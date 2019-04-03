using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public enum PlayerTeam { none, human, badger }
        [SerializeField] public PlayerTeam Team { get; set; }

        GameManager gameManager;
        HealthBar healthBar;
        [SerializeField]
        Material defaultMaterial;
        [SerializeField]
        Material redMaterial;
        [SerializeField] GameObject GFX;
        public float Health { get; set; } = 100f;
        public int playerNumber;
        [SerializeField] int joystick;
        [SerializeField] PlayerTeam t;
        public bool dead = false;

        [SerializeField] AudioSource audioSource;
        [SerializeField] List<AudioClip> specificDeathSound = new List<AudioClip>();


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
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
            player.GetComponent<NewControllerInputs>().SetJoystickNumber(joystickNum);

            return player;
        }

        IEnumerator ColourTime() // the players will flash red
        {
            GFX.GetComponent<Renderer>().material = redMaterial; //the material of the GFX will change to a red material
            yield return new WaitForSeconds(0.1f);  // a timer of 0,1 seconds will start before the next code start
            GFX.GetComponent<Renderer>().material = defaultMaterial; //the material of the GFX will change to its original material
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            StartCoroutine(ColourTime());  //the coroutine with the materialchanges will start here
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
            audioSource.clip = specificDeathSound[Random.Range(0, specificDeathSound.Count)];
            audioSource.Play();
            dead = true;
            gameObject.GetComponent<NewControllerInputs>().DropWeapon();
            gameManager.TeamIsDead((PlayerTeam)playerNumber);

            Destroy(gameObject);

        }
    }
}