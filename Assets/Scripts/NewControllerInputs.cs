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

        // inputs for controller movement
        string horizontalAxis;
        string verticalAxis;
        string aButton;
        string bButton;
        string xButton;
        string shootButton;
        int controllerNumber;

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

        public void SetControllerNumber(int number) //sets the controller input to the right controller
        {
            controllerNumber = number;
            horizontalAxis = "HorizontalController" + playerNumber.ToString();
            verticalAxis = "VerticalController" + playerNumber.ToString();
            aButton = "JumpController" + playerNumber.ToString();
            bButton = "DropWeapon" + playerNumber.ToString();
            xButton = "WeaponPickUp" + playerNumber.ToString();
            shootButton = "WeaponFireController" + playerNumber.ToString();
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
            movementScript.MovDir = Input.GetAxisRaw(horizontalAxis);

            //jump
            if (Input.GetButtonDown(aButton))
            {
                Debug.Log(playerNumber + " " + jumpScript.gameObject.name);
                jumpScript.Jump();
            }

            //drop
            if (Input.GetAxis(verticalAxis) <= -0.8f)
            {
                jumpScript.DropThrough();

            }

            //shoot
            if (Input.GetButton(shootButton) && currentWeapon != null)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = true;
            }
            else if (currentWeapon != null && currentWeapon.GetComponent<IWeapon>().Firing)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = false;
            }

            //pick up weapon
            if (Input.GetButtonDown(xButton))
            {
                PickUpWeapon();
            }

            //drop weapon
            if (Input.GetButtonDown(bButton)) //testing, change button later
            {
                DropWeapon();
            }
        }
    }
}
