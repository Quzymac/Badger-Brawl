using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class NewControllerInputs : MonoBehaviour
    {
        GameObject currentWeapon;
        GameObject canPickUp;
        Rigidbody rb;
        JumpScript jumpScript;
        ControllerMovement controllerMovement;
        [SerializeField] Transform holdPosition; // players hand holding the weapon

        public int JoystickNumber { get; set; }

        // inputs for controller movement
        string horizontalAxis;
        string verticalAxis;
        string aButton;
        string bButton;
        string xButton;
        string shootButton;



        // Start is called before the first frame update
        void Start()
        {
            SetJoystickNumber(JoystickNumber);
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            controllerMovement = GetComponent<ControllerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }
            PlayerControlls();
        }

        public void SetJoystickNumber(int joystick) //sets the controller input to the right controller
        {
            JoystickNumber = joystick;
            horizontalAxis = "HorizontalController" + JoystickNumber.ToString();
            verticalAxis = "VerticalController" + JoystickNumber.ToString();
            aButton = "JumpController" + JoystickNumber.ToString();
            bButton = "DropWeapon" + JoystickNumber.ToString();
            xButton = "WeaponPickUp" + JoystickNumber.ToString();
            shootButton = "WeaponFireController" + JoystickNumber.ToString();
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
                currentWeapon.transform.position = holdPosition.position;
                //currentWeapon.transform.rotation = holdPosition.rotation;

                currentWeapon.transform.parent = transform;
                currentWeapon.GetComponent<Collider>().enabled = false;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                currentWeapon.GetComponent<IWeapon>().Owner = gameObject;
                

                //ändrar rotation på vapnet när man plockar upp det. Buggar däremot ibland. Ej löst.
                if (currentWeapon.transform.rotation.y < currentWeapon.transform.parent.rotation.y)
                {
                    Debug.Log("rotating");
                    currentWeapon.transform.Rotate(0, 180, 0);
                }
                
                else if (currentWeapon.transform.rotation.y > currentWeapon.transform.parent.rotation.y)
                {
                    Debug.Log("Rotaded other way");
                    currentWeapon.transform.Rotate(0, -180, 0);                  
                }
                //else if (currentWeapon.transform.rotation.y < 0 && currentWeapon.transform.parent.rotation.y < 0)
                //{
                //    Debug.Log("now we got here");
                //    currentWeapon.transform.Rotate(0, 180, 0);
                //}
            }
        }
        public void DropWeapon()
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
        
        void PlayerControlls()
        {
            //calculate movement velocity
            controllerMovement.MoveDir = Input.GetAxisRaw(horizontalAxis);

            //jump
            if (Input.GetButtonDown(aButton))
            {
                Debug.Log(JoystickNumber + " " + jumpScript.gameObject.name);
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
