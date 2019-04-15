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
        public bool dead { get; set; } = false;
        MultipleTargetCam multipleTargetCam;

        Rigidbody rb;
        public bool falling  = false;
        JumpScript jumpScript;
        AnimationHandler animationHandler;
        Collider col;

        private void Start()
        {
            col = GetComponent<Collider>();
            multipleTargetCam = FindObjectOfType<MultipleTargetCam>();
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            animationHandler = GetComponent<AnimationHandler>();
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
            dead = true;
            gameManager.soundManager.PlayDeathSounds(this);
            gameObject.GetComponent<NewControllerInputs>().DropWeapon();
            gameManager.TeamIsDead((PlayerTeam)playerNumber);
            multipleTargetCam.targets.Remove(transform);
            Destroy(gameObject);

        }

        public void CheckVelocity()
        {
            if (rb.velocity.y < 0 && falling == false && jumpScript.grounded == false)
            {
                if (jumpScript.Running == true)
                {
                    animationHandler.RunningFalling(); //animation från spring til falla
                }
                falling = true;
            }
            //if (rb.velocity.y < 0 && falling == true)
            //{
            //    if (jumpScript.Running == false)
            //    {
            //        animationHandler.IdleFalling();
            //    }            
            //}
        }

        private void Update()
        {
            CheckVelocity();
        }
    }
}