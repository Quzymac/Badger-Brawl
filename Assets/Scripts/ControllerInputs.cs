using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class ControllerInputs : MonoBehaviour
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
        
        bool lookingRight = true;

        AnimationHandler animationHandler;
        Animator anim;
        PlayerScript playerScript;

        RaycastHit hit;
        int groundLayer;

        IKHandler ikHandler;

        [SerializeField] Transform aimCenter;
        [SerializeField] Transform aimTowards;

        Vector2 aimInput;
        float rotationInputThreshold = 0.6f;

        void Start()
        {
            ikHandler = GetComponent<IKHandler>();
            anim = GetComponent<Animator>();
            groundLayer = LayerMask.NameToLayer("PlatformJumpThrough");
            playerScript = GetComponent<PlayerScript>();
            animationHandler = GetComponent<AnimationHandler>();
            rb = GetComponent<Rigidbody>();
            jumpScript = GetComponent<JumpScript>();
            controllerMovement = GetComponent<ControllerMovement>();
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
            if (canPickUp != null && canPickUp.GetComponent<IWeapon>().Owner == null)
            {
                if (currentWeapon != null)
                {
                    DropWeapon();
                }
                
                currentWeapon = canPickUp.gameObject;
                currentWeapon.transform.position = aimTowards.position;
                currentWeapon.transform.rotation = aimTowards.rotation;

                currentWeapon.transform.parent = aimTowards;
                
                currentWeapon.GetComponent<Collider>().enabled = false;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                currentWeapon.GetComponent<IWeapon>().Owner = gameObject;
                currentWeapon.GetComponent<WeaponDespawn>().StopDespawn();

                ikHandler.RightHand = currentWeapon.GetComponent<IWeapon>().RightHand;
                ikHandler.LeftHand = currentWeapon.GetComponent<IWeapon>().LeftHand;
                animationHandler.PickUp();
                canPickUp = null;
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

                ikHandler.RightHand = null;
                ikHandler.LeftHand = null;

                currentWeapon = null;
                animationHandler.DropWeapon();
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
        private void OnTriggerStay(Collider other)
        {
            if (canPickUp == null && other.CompareTag("Weapon"))
            {
                canPickUp = other.gameObject;
            }
        }

        void PlayerControlls()
        {
            //Rotate to look left/ right
            if (lookingRight && Input.GetAxis(aimHorizontal) < -rotationInputThreshold)
            {
                transform.Rotate(new Vector3(0, -180, 0));
                lookingRight = false;
            }
            else if (!lookingRight && Input.GetAxis(aimHorizontal) > rotationInputThreshold)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                lookingRight = true;
            }

            //AIMING
            aimInput = new Vector2(Input.GetAxis(aimHorizontal), Input.GetAxis(aimVertical));

            if (aimInput.magnitude > 0.5f)
            {
                if (lookingRight)
                {
                    aimCenter.rotation = Quaternion.Euler(transform.rotation.x, 0, aimInput.y * 89f);
                }
                else
                {
                    aimCenter.rotation = Quaternion.Euler(transform.rotation.x, 180, aimInput.y * 89f);
                }
            }

            //calculate movement velocity
            controllerMovement.MoveDir = Input.GetAxisRaw(horizontalAxis);


            //jump
            if (Input.GetButtonDown(aButton))
            {
                if (animationHandler.Jumping == false)
                {
                    Debug.Log(jumpScript.Running);
                    if (jumpScript.Running == false)
                    {
                        animationHandler.IdleToJump();
                    }
                    else if (jumpScript.Running == true)
                    {
                        animationHandler.RunToJump(); //SPelar inte hopp animationen om spelaren vill springa i luften
                    }
                }
                else if (animationHandler.Jumping == true && jumpScript.jumpCount > 0)
                {
                    Debug.Log("Dubble jump animation");
                    animationHandler.FallingJumpingCheck();
                }
                jumpScript.Jump();
            }

            //drop
            if (Input.GetAxis(verticalAxis) <= -0.8f)
            {
               jumpScript.DropThrough();
            }
            //shoot
            if (Input.GetAxisRaw(shootButton)> 0.2f && currentWeapon != null)
            {
                currentWeapon.GetComponent<IWeapon>().Firing = true;
                if (GetComponentInChildren<IWeapon>().typeOfWeapon == TestGun.TypeOfWeapon.throwable)
                {
                    anim.SetTrigger("Throwing");
                }
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
            if (Input.GetButtonDown(bButton))
            {
                DropWeapon();
            }
        }
    }
}
