using UnityEngine;

namespace Player
{
    public class JumpScript : MonoBehaviour
    {
        Rigidbody rb;
        Collider playerCollider;
        NewControllerInputs controllerInputs;
        GameObject collidedPlatform;
        public bool grounded;
        float jumpTime;
        public float jumpCount;
        [SerializeField] float jumpForce = 700f;
        [SerializeField] float gravity = 16f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float lowJumpMultiplier = 2f;
        LayerMask platformLayer;
        Vector3 jumpDir;
        PlayerScript player;

        [SerializeField] AudioClip jumpSound;
        [SerializeField] AudioClip landingSound;
        [SerializeField] AudioSource audioSource;
        public bool landed = false;

        void Start()
        {
            player = GetComponent<PlayerScript>();
            audioSource = GetComponent<AudioSource>();
            platformLayer |= (1 << LayerMask.NameToLayer("Platform"));
            platformLayer |= (1 << LayerMask.NameToLayer("PlatformJumpThrough"));


            rb = GetComponent<Rigidbody>();
            playerCollider = GetComponent<Collider>();
            controllerInputs = GetComponent<NewControllerInputs>();

            jumpDir = new Vector3(0, jumpForce, 0);
            Physics.gravity = new Vector3(0, -gravity, 0);
        }

        public void Jump()
        {
            if (jumpCount > 0)
            {
                audioSource.clip = jumpSound;
                audioSource.Play();
                if (!grounded && Time.time > jumpTime)
                    jumpCount--;
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                rb.AddForce(jumpDir);
                gameObject.layer = 12;
                grounded = false;
            }
        }

        public void DropThrough()
        {
            if (collidedPlatform != null && collidedPlatform.layer == 13 && collidedPlatform.transform.position.y <= gameObject.transform.position.y)
            {
                Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, true);
            }
        }
        private void FixedUpdate()
        {
            //better jump, hålla in "hopp" för längre hopp
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && (!Input.GetKey(KeyCode.UpArrow) && !Input.GetButton("JumpController" + controllerInputs.JoystickNumber.ToString()))) //testing, change button later------------------------------------------------------------
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        void Update()
        {
            GroundDetection();
            if (landed == true)
            {
                audioSource.clip = landingSound;
                audioSource.volume = 0.2f;
                audioSource.Play();
                landed = false;
                player.falling = false;
            }

            if (rb.velocity.y < 0 && gameObject.layer == 12 && collidedPlatform != null)
            {
                gameObject.layer = 10;
                Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
            }
            else if (rb.velocity.y < 0 && gameObject.layer == 10)
            {
                if (collidedPlatform != null && collidedPlatform.transform.position.y >= gameObject.transform.position.y)
                {
                    Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
                }
            }
            else if (rb.velocity.y > 0 && collidedPlatform != null)
            {
                Physics.IgnoreCollision(collidedPlatform.GetComponent<Collider>(), playerCollider, false);
            }
        }

        private void GroundDetection()
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
                
            } else
            {
                if (grounded)
                {
                    jumpTime = Time.time + 0.2f;
                }
                grounded = false;
            }

            if (player.falling == true && landed == false && grounded == true)
            {
                landed = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            collidedPlatform = collision.gameObject;
        }
    }
}
