using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class NewControllerInputs : MonoBehaviour
    {
        GameObject currentWeapon;
        GameObject canPickUp;
        public int playerNumber;
        Rigidbody rb;
        JumpScript jumpScript;
        ControllerMovement movementScript;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            movementScript = GetComponent<ControllerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerControlls();
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

        void PlayerControlls()
        {
            //calculate movement velocity
            movementScript.MovDir = Input.GetAxisRaw("HorizontalController" + playerNumber.ToString());

            //jump
            if (Input.GetButtonDown("JumpController" + playerNumber.ToString()))
            {
                Debug.Log(playerNumber + " " + jumpScript.gameObject.name);
                jumpScript.Jump();
            }

            //drop
            if (Input.GetAxis("VerticalController" + playerNumber.ToString()) <= -0.8f)
            {
                jumpScript.DropThrough();

            }

            //shoot
            if (Input.GetButton("WeaponFireController" + playerNumber.ToString()) && currentWeapon != null)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = true;
            }
            else if (currentWeapon != null && currentWeapon.GetComponent<IWeapon>().Firing)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = false;
            }

            //pick up weapon
            if (Input.GetButtonDown("WeaponPickUp" + playerNumber.ToString()))
            {
                PickUpWeapon();
            }

            //drop weapon
            if (Input.GetButtonDown("DropWeapon" + playerNumber.ToString())) //testing, change button later
            {
                DropWeapon();
            }
        }
    }
}
