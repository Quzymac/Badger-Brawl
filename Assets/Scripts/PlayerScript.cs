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
        public bool dead { get; set; } = false;
        MultipleTargetCam multipleTargetCam;
        public bool takeDmgOverTime;
        Rigidbody rb;
        public bool falling  = false;
        JumpScript jumpScript;
        AnimationHandler animationHandler;
        [SerializeField] List<GameObject> deathParticle = new List<GameObject>();
        [SerializeField] List<Material> specificMaterial = new List<Material>();
        [SerializeField] GameObject PolySurface;
        Renderer mat;

        private void Start()
        {
            multipleTargetCam = FindObjectOfType<MultipleTargetCam>();
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            animationHandler = GetComponent<AnimationHandler>();
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            healthBar = FindObjectOfType<HealthBarManager>().GetComponent<HealthBarManager>().healthBars[playerNumber - 1];
            if (Team == PlayerTeam.badger)
            {
                healthBar.badger.enabled = true;
            }
            if (Team == PlayerTeam.human)
            {
                healthBar.human.enabled = true;
            }

            mat = PolySurface.GetComponent<Renderer>();
            mat.material = specificMaterial[playerNumber - 1];

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
            GFX.GetComponent<Renderer>().material = specificMaterial[playerNumber - 1]; //the material of the GFX will change to its original material
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
        }

        void Death()
        {
            dead = true;
            gameManager.soundManager.PlayDeathSounds(this);
            gameObject.GetComponent<NewControllerInputs>().DropWeapon();
            if(Team == PlayerTeam.human)
            {
                gameManager.allHumansDead = gameManager.TeamIsDead(Team);
            }
            if (Team == PlayerTeam.badger)
            {
                gameManager.allBadgersDead = gameManager.TeamIsDead(Team);
            }
            multipleTargetCam.targets.Remove(transform);
            Instantiate(deathParticle[playerNumber - 1], transform.position, transform.rotation);
            Destroy(gameObject);
        }

        public void CheckVelocity()
        {
            if (rb.velocity.y < 0 && falling == false && jumpScript.grounded == false)
            {
                falling = true;
                animationHandler.JumpingToFalling();
            }
        }

        private void Update()
        {
            CheckVelocity();
        }
        private void FixedUpdate()
        {
            if (takeDmgOverTime == true)
            {
                TakeDamage(0.5f);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "DamageOverTime")
            {

                takeDmgOverTime = true;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "DamageOverTime")
            {
                takeDmgOverTime = false;
            }
        }
    }
}