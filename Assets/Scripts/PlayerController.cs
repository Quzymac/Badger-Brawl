using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IWeapon
    {
        float Damage { get; }
        float ShotsPerSecond { get; }
        float ProjectileSpeed { get; }

        bool Firing { get; set; }

        void Fire();
        GameObject Owner { get; set; }
    }

    public class PlayerController : MonoBehaviour
    {
        GameObject currentWeapon;
        GameObject canPickUp;

        [SerializeField] float jumpForce = 700f;
        [SerializeField] float gravity = 16f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float lowJumpMultiplier = 2f;
        [SerializeField] bool onGround = true;
        [SerializeField] bool canDoubleJump = false;
        [SerializeField] LayerMask platformLayer;
        Rigidbody rb;
        Vector3 jumpDir;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            //move to start when testing is done --v
            jumpDir = new Vector3(0, jumpForce, 0);
            Physics.gravity = new Vector3(0, -gravity, 0);


            //better jump, hålla in "hopp" för längre hopp
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))//testing, change button later
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        // Update is called once per frame
        void Update()
        {
            GroundDetection();
            //jump
            if (Input.GetKeyDown(KeyCode.UpArrow))//testing, change button later
            {
                if (onGround)
                {
                    //jumpCount++;
                    canDoubleJump = true;
                    rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                    rb.AddForce(jumpDir);
                }

                else if (!onGround && canDoubleJump)
                {
                    canDoubleJump = false;
                    rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                    rb.AddForce(jumpDir);
                }
            }

            //shoot
            if (Input.GetKey(KeyCode.Space) && currentWeapon != null)//testing, change button later
            {
                currentWeapon.GetComponent<IWeapon>().Firing = true;
            }
            else if (currentWeapon != null && currentWeapon.GetComponent<IWeapon>().Firing)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = false;
            }

            //pick up
            if (Input.GetKeyDown(KeyCode.M)) //testing, change button later
            {
                PickUpWeapon();
            }

            //drop
            if (Input.GetKeyDown(KeyCode.N)) //testing, change button later
            {
                DropWeapon();
            }
        }
        void PickUpWeapon()
        {
            if (canPickUp != null)
            {
                if (currentWeapon != null)
                {
                    DropWeapon();
                }

                currentWeapon = canPickUp.gameObject;
                currentWeapon.transform.position = transform.position;
                currentWeapon.transform.rotation = transform.rotation;

                currentWeapon.transform.parent = transform;
                currentWeapon.GetComponent<Collider>().enabled = false;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                currentWeapon.GetComponent<IWeapon>().Owner = gameObject;
            }
        }
        void DropWeapon()
        {
            if (currentWeapon != null)
            {
                currentWeapon.GetComponent<Collider>().enabled = true;
                currentWeapon.GetComponent<Rigidbody>().useGravity = true;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                currentWeapon.transform.parent = null;
                currentWeapon.GetComponent<IWeapon>().Owner = null;

                currentWeapon = null;
            }
        }
        private void GroundDetection()
        {
            RaycastHit hit;
            Vector2 physicsCentre = transform.position + this.GetComponent<SphereCollider>().center;
            Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red, 1);

            if (Physics.Raycast(physicsCentre, Vector2.down, out hit, 0.6f, platformLayer))
            {
                canDoubleJump = false;
                onGround = true;
            }
            else
            {
                if (Physics.Raycast(transform.position, new Vector2(-1, -1), out hit, 0.7f, platformLayer) || Physics.Raycast(transform.position, new Vector2(1, -1), out hit, 0.7f, platformLayer))
                {
                    Debug.DrawRay(transform.position, new Vector2(-1, -1) * 0.7f, Color.blue, 1);
                    Debug.DrawRay(transform.position, new Vector2(1, -1) * 0.7f, Color.green, 1);

                    canDoubleJump = false;
                    onGround = true;
                }
                else
                    onGround = false;
            }
            Debug.Log(onGround);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon")) //Testing only
            {
                canPickUp = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon")) //Testing only
            {
                canPickUp = null;
            }
        }
        public void TakeDamage(float damage)
        {
            Debug.Log(damage + " damage taken");
        }
    }
}