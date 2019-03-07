using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IWeapon
    {
        float Damage { get; }
        float FireRate { get; }

        void Fire();
        GameObject Owner { get; set; }


    }
    public class PlayerController : MonoBehaviour
    {
        GameObject weapon;
        GameObject canPickUp;

        [SerializeField] float jumpForce = 500f;
        [SerializeField] float gravity = 25f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float lowJumpMultiplier = 2f;

        int jumpCount;

        Rigidbody rb;
        Vector3 jumpDir;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //move to start when testing is done --v
            jumpDir = new Vector3(0, jumpForce, 0);
            Physics.gravity = new Vector3(0, -gravity, 0);


            //better jump, hålla in "hopp" för längre hopp
            if(rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
            {
                jumpCount++;
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                rb.AddForce(jumpDir);
            }
            if (Input.GetKeyDown(KeyCode.W) && jumpCount < 2)
            {
                jumpCount++;
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                rb.AddForce(jumpDir, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (weapon != null)
                {
                    weapon.GetComponent<IWeapon>().Fire();
                }
            }

            if (Input.GetKeyDown(KeyCode.M)) //testing
            {
                PickUpWeapon();
            }

            if (Input.GetKeyDown(KeyCode.N)) //testing
            {
                DropWeapon();
            }
        }
        void PickUpWeapon()
        {
            if(canPickUp != null)
            {
                if(weapon != null)
                {
                    DropWeapon();
                }
                weapon = canPickUp.gameObject;
                weapon.transform.position = transform.position;
                weapon.transform.rotation = transform.rotation;

                weapon.transform.parent = transform;
                weapon.GetComponent<Collider>().enabled = false;
                weapon.GetComponent<Rigidbody>().useGravity = false;
                weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            }
        }
        void DropWeapon()
        {
            if (weapon != null)
            {
                weapon.GetComponent<Collider>().enabled = true;
                weapon.GetComponent<Rigidbody>().useGravity = true;
                weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                weapon.transform.parent = null;
                weapon = null;
            }
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
        private void OnCollisionEnter(Collision c)
        {
            Vector2 platformMiddle = c.transform.position;
            float top = platformMiddle.y + (c.transform.localScale.y / 2);

            if (transform.position.y - top > 0.08f)
            {
                Debug.Log(transform.position.y - top);
                jumpCount = 0;
            }
        }
        public void TakeDamage(float damage)
        {
            Debug.Log(damage + " damage taken");
        }
    }
}
