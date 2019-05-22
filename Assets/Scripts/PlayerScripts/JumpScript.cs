using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player
{
    public class JumpScript : MonoBehaviour
    {
        PlayerScript player;

        Rigidbody rb;
        Collider playerCollider;
        ControllerInputs controllerInputs;
        GameObject collidedPlatform;
        public bool Running { get; set; }
        public bool grounded;
        public bool fallThrough = false;
        public bool landed = false;

        public float jumpCount;
        float jumpTime;
        [SerializeField] float jumpForce = 700f;
        [SerializeField] float gravity = 16f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float lowJumpMultiplier = 2f;

        LayerMask platformLayer;
        LayerMask jumpThroughLayer;

        Vector3 jumpDir;
        Vector3 colloffset = new Vector3(0, 0.3f, 0);

        [SerializeField] AudioClip jumpSound;
        [SerializeField] AudioClip landingSound;
        [SerializeField] AudioSource audioSource;
        


        AnimationHandler animationHandler;

        void Start()
        {
            player = GetComponent<PlayerScript>();
            audioSource = GetComponent<AudioSource>();
            animationHandler = GetComponent<AnimationHandler>();
            platformLayer |= (1 << LayerMask.NameToLayer("Platform"));
            platformLayer |= (1 << LayerMask.NameToLayer("PlatformJumpThrough"));
            jumpThroughLayer |= (1 << LayerMask.NameToLayer("PlatformJumpThrough"));

            rb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<Collider>();
            controllerInputs = GetComponent<ControllerInputs>();

            jumpDir = new Vector3(0, jumpForce, 0);
            Physics.gravity = new Vector3(0, -gravity, 0);
        }

        public void Jump()
        {
            if (jumpCount > 0)
            {
                animationHandler.Jumping = true;
                audioSource.clip = jumpSound;
                audioSource.Play();
                if (!grounded && Time.time > jumpTime)
                    jumpCount--;
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                rb.AddForce(jumpDir);
                grounded = false;
            }
        }

        public void DropThrough()
        {
            gameObject.layer = 12;
        }
        
        private void FixedUpdate()
        {
            //better jump, hålla in "hopp" för längre hopp
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("JumpController" + controllerInputs.JoystickNumber.ToString()))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        void CheckRunning()
        {
            if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1)
            {
                Running = true;
            }
            else if (rb.velocity.x < 0.1 || rb.velocity.x > -0.1)
            {
                Running = false;
            }
        }
        void Update()
        {
            Collider[] hits = Physics.OverlapBox(transform.position + colloffset, new Vector3(0.5f, 0.7f, 0.2f), Quaternion.identity, jumpThroughLayer);
            if (hits.Length == 0 && fallThrough == false)
            {
                gameObject.layer = 10;
            }
            else if (hits.Length > 0)
            {
                gameObject.layer = 12;
                fallThrough = false;
            }
            CheckRunning();
            GroundDetection();
            CheckLanded();
        }
        private bool GroundDetection()
        {
            RaycastHit hit;
            Vector2 physicsCentre = transform.position + this.GetComponent<CapsuleCollider>().center;

            if (Physics.Raycast(physicsCentre, Vector2.down, out hit, 1.1f, platformLayer) && rb.velocity.y <= 0)
            {
                grounded = true;
                jumpCount = 1;
            }
            else if (Physics.Raycast(transform.position, new Vector2(-1, -1), out hit, 1.2f, platformLayer) && rb.velocity.y <= 0 || Physics.Raycast(transform.position, new Vector2(1, -1), out hit, 1.2f, platformLayer) && rb.velocity.y <= 0)
            {
                grounded = true;
                jumpCount = 1;               
            }
            else
            {
                if (grounded)
                {
                    jumpTime = Time.time + 0.2f;
                }
                grounded = false;
            }

            if (player.falling == true && landed == false && grounded == true )
            {
                landed = true;
            }
            return grounded;
        }      
        
        private void OnCollisionEnter(Collision collision)
        {
            collidedPlatform = collision.gameObject;
        }

        public bool CheckLanded()
        {
            if (landed == true)
            {
                audioSource.clip = landingSound;
                audioSource.volume = 0.2f;
                audioSource.Play();
                player.falling = false;
                animationHandler.Jumping = false;
                animationHandler.FallingToLanding();
                landed = false;

                if (Running == true)
                {
                    animationHandler.LandingToRunning();
                }
                else if (Running == false)
                {
                    animationHandler.LandingToIdle();
                }
            }
            return landed;
        }
    }
}
