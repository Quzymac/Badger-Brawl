using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    

    public class PlayerController : MonoBehaviour
    {
        GameObject currentWeapon;
        GameObject canPickUp;
       
        Rigidbody rb;
        JumpScript jumpScript;
        public PlayerMovement playerMovement { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {

            //calculate movement velocity
            playerMovement.MoveDir = Input.GetAxisRaw("Horizontal");

            //jump
            if (Input.GetKeyDown(KeyCode.UpArrow))//testing, change button later
            {
                jumpScript.Jump();
            }
            
            //drop
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                jumpScript.DropThrough();
            }

            //shoot
            if (Input.GetKey(KeyCode.Space) && currentWeapon != null) //testing, change button later
            {
                currentWeapon.GetComponent<IWeapon>().Firing = true;
            }
            else if (currentWeapon != null && currentWeapon.GetComponent<IWeapon>().Firing)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = false;
            }

            //pick up weapon
            if (Input.GetKeyDown(KeyCode.M)) //testing, change button later
            {
                PickUpWeapon();
            }

            //drop weapon
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