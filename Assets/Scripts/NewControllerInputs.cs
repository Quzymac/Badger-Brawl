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

        string aimHorizontal;
        string aimVertical;

        float holdPosXRotationQuat;
        float holdPosYRotationEuler;
        float holdPosZRotationEuler;
        
        Quaternion targetAngle = new Quaternion(1, 0, 0, 0); //right at start
        float aimSpeed = 9f;

        bool lookingRight = true;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            controllerMovement = GetComponent<ControllerMovement>();

            holdPosXRotationQuat = holdPosition.localRotation.x;
            holdPosYRotationEuler = holdPosition.localRotation.eulerAngles.y;
            holdPosZRotationEuler = holdPosition.localRotation.eulerAngles.z;
        }

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
            aimHorizontal = "AimHorizontal" + JoystickNumber.ToString();
            aimVertical = "AimVertical" + JoystickNumber.ToString();
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
                currentWeapon.transform.rotation = holdPosition.rotation;

                currentWeapon.transform.parent = holdPosition;
                currentWeapon.GetComponent<Collider>().enabled = false;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                currentWeapon.GetComponent<IWeapon>().Owner = gameObject;
               
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
            if (other.CompareTag("Weapon"))
            {
                canPickUp = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                canPickUp = null;
            }
        }
        bool canJump = true; //testing
        void PlayerControlls()
        {
            //aiming  WIP
            Vector3 aimDir;
            if (lookingRight)
            {
                aimDir = Vector3.forward * -Input.GetAxisRaw(aimHorizontal) + Vector3.up * -Input.GetAxisRaw(aimVertical);
            }
            else
            {
                aimDir = Vector3.back * -Input.GetAxisRaw(aimHorizontal) + Vector3.up * -Input.GetAxisRaw(aimVertical);
            }
            if (aimDir.sqrMagnitude > 0.5f) //aim sensitivity when to apply rotation
            {
                targetAngle = Quaternion.LookRotation(aimDir, Vector3.down);
                targetAngle = Quaternion.Euler(targetAngle.eulerAngles.x, holdPosYRotationEuler, holdPosZRotationEuler); //aim staright, set y and z to start rotation
            }

            //Rotate to look left/ right
            if (lookingRight && Input.GetAxis(aimHorizontal) < 0)
            {
                transform.Rotate(new Vector3(0, -180, 0));
                lookingRight = false;
            }
            if (!lookingRight && Input.GetAxis(aimHorizontal) > 0)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                lookingRight = true;
            }
            //Rotate Aim
            if (holdPosition.transform.localRotation != targetAngle)
            {
                holdPosition.transform.localRotation = Quaternion.Lerp(holdPosition.transform.localRotation, targetAngle, Time.deltaTime * aimSpeed);
            }


            //Aim up and down
            //if (aimDir.sqrMagnitude > 0.5f)
            //{
            //    holdPosition.Rotate(new Vector3(Input.GetAxis(aimVertical) * -5, 0, 0), Space.Self);
            //    float aim = Input.GetAxis(aimVertical) * -90;


            //    //float aim = Input.GetAxis(aimVertical) * -90;
            //    //holdPosition.localRotation = Quaternion.Euler(new Vector3(holdPosXRotation + aim + 180f, holdPosition.localRotation.y, holdPosition.localRotation.z));
            //}


            //calculate movement velocity
            controllerMovement.MoveDir = Input.GetAxisRaw(horizontalAxis);

            //jump
            if (Input.GetButtonDown(aButton))
            {
                jumpScript.Jump();

            }
            //if (Input.GetAxis("j") > 0.8f && canJump)
            //{
            //    Debug.Log(JoystickNumber + " " + jumpScript.gameObject.name);
            //    jumpScript.Jump();
            //    canJump = false;
            //}
            //if (Input.GetAxis("j") < 0.8f && !canJump)
            //{
            //    canJump = true;
            //}

            //drop
            if (Input.GetAxis(verticalAxis) <= -0.8f)
            {
                jumpScript.DropThrough();

            }
            //shoot
            if (Input.GetAxisRaw(shootButton)> 0.2f && currentWeapon != null)
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
