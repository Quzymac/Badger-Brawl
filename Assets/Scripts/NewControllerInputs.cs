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

        AnimationHandler animationHandler;
        Animator anim;
        PlayerScript playerScript;

        RaycastHit hit;
        int groundLayer;

        IKHandler ikHandler;
        private void Awake()
        {
            SetJoystickNumber(1);//FORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTINGFORTESTING

        }
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
                currentWeapon.transform.position = aimTowards.position;
                currentWeapon.transform.rotation = aimTowards.rotation;

                currentWeapon.transform.parent = aimTowards;
                //currentWeapon.transform.position = holdPosition.position;
                //currentWeapon.transform.rotation = holdPosition.rotation;

                //currentWeapon.transform.parent = holdPosition;
                currentWeapon.GetComponent<Collider>().enabled = false;
                currentWeapon.GetComponent<Rigidbody>().useGravity = false;
                currentWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                currentWeapon.GetComponent<IWeapon>().Owner = gameObject;

                ikHandler.RightHand = currentWeapon.GetComponent<IWeapon>().RightHand;
                ikHandler.LeftHand = currentWeapon.GetComponent<IWeapon>().LeftHand;
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

        [SerializeField] Transform aimCenter;
        [SerializeField] Transform aimTowards;
        [SerializeField] Transform chest;

        [SerializeField] Vector3 offset;

        Vector2 aimInput;
        Quaternion rotation;
        float rotationInputThreshold = 0.6f;
        
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

            //AIMING V.5
            aimInput = new Vector2(Input.GetAxis(aimHorizontal), Input.GetAxis(aimVertical));

            if (aimInput.magnitude > 0.5f && Mathf.Abs(Input.GetAxis(aimHorizontal)) > rotationInputThreshold) //minimum input for aim && cant aim straigt up or down to fix rotation bug
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
        


            ////Aiming V.4
            //aimInput = new Vector2(Input.GetAxis(aimHorizontal), Input.GetAxis(aimVertical));

            //if (aimInput.magnitude > 0.5f && Mathf.Abs(Input.GetAxis(aimHorizontal)) > rotationInputThreshold) //minimum input for aim && cant aim straigt up or down to fix rotation bug
            //{
            //    if (lookingRight)
            //    {
            //        aimCenter.rotation = Quaternion.Euler(0, 0, aimInput.y * 89f);
            //    }
            //    else
            //    {
            //        aimCenter.rotation = Quaternion.Euler(0, 180, aimInput.y * 89f);
            //    }
            //}
            //rotation = Quaternion.LookRotation(aimTowards.position - chest.position, Vector3.up);
            //rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0); //reset z-rotation
            //chest.rotation = rotation;
            //chest.Rotate(new Vector3(0, 0, -90f));
            //chest.rotation = chest.rotation * Quaternion.Euler(offset);


            //// Aiming V.3
            //aimInput = new Vector2(Input.GetAxis(aimHorizontal), Input.GetAxis(aimVertical));

            //if (aimInput.magnitude > rotationInputThreshold && Mathf.Abs(Input.GetAxis(aimHorizontal)) > rotationInputThreshold) //minimum input for aim && cant aim straigt up or down to fix rotation bug
            //{
            //    aimDir = holdPosition.position + new Vector3(aimInput.x, aimInput.y, 0).normalized; 
            //    aimTowards.position = aimDir;
            //    rotation = Quaternion.LookRotation(aimTowards.position - holdPosition.position, Vector3.up);
            //    rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0); //reset z-rotation
            //}
            //holdPosition.rotation = Quaternion.Lerp(holdPosition.rotation, rotation, aimSpeed * Time.deltaTime); //lerp rotation
            ////holdPosition.rotation = rotation; //instant rotation


            ////aiming  V.1
            //Vector3 aimDir;
            //if (lookingRight)
            //{
            //    aimDir = Vector3.forward * -Input.GetAxisRaw(aimHorizontal) + Vector3.up * -Input.GetAxisRaw(aimVertical);
            //}
            //else
            //{
            //    aimDir = Vector3.back * -Input.GetAxisRaw(aimHorizontal) + Vector3.up * -Input.GetAxisRaw(aimVertical);
            //}
            //if (aimDir.sqrMagnitude > 0.5f) //aim sensitivity when to apply rotation
            //{
            //    targetAngle = Quaternion.LookRotation(aimDir, Vector3.down);
            //    targetAngle = Quaternion.Euler(targetAngle.eulerAngles.x, holdPosYRotationEuler, holdPosZRotationEuler); //aim staright, set y and z to start rotation
            //}

            ////Rotate Aim
            //if (holdPosition.transform.localRotation != targetAngle)
            //{
            //    holdPosition.transform.localRotation = Quaternion.Lerp(holdPosition.transform.localRotation, targetAngle, Time.deltaTime * aimSpeed);
            //}

            //Aim V.  up and down
            //if (aimDir.sqrMagnitude > 0.5f)
            //{
            //    holdPosition.Rotate(new Vector3(Input.GetAxis(aimVertical) * -5, 0, 0), Space.Self);
            //    float aim = Input.GetAxis(aimVertical) * -90;


            //    //float aim = Input.GetAxis(aimVertical) * -90;
            //    //holdPosition.localRotation = Quaternion.Euler(new Vector3(holdPosXRotation + aim + 180f, holdPosition.localRotation.y, holdPosition.localRotation.z));
            //}


            //calculate movement velocity
            controllerMovement.MoveDir = Input.GetAxisRaw(horizontalAxis);


            if ((Input.GetAxisRaw(horizontalAxis) > 0 || Input.GetAxisRaw(horizontalAxis) < 0) && jumpScript.grounded == true && animationHandler.IdleBool == true)
            {
                animationHandler.IdleBool = false;
                animationHandler.IdleRun();
            }
            else if(Input.GetAxisRaw(horizontalAxis) == 0 && jumpScript.grounded == true && animationHandler.IdleBool == false)
            {
                animationHandler.IdleBool = true;
                animationHandler.RunningIdle();
                Debug.Log("Runnning idle pls");
            }
            //else if (jumpScript.grounded == false && rb.velocity.y > 0 || jumpScript.grounded == false && rb.velocity.y < 0)
            //{
            //    animationHandler.IdleBool = false;
            //}
           

            //jump
            if (Input.GetButtonDown(aButton))
            {

                if (animationHandler.IdleBool == true)
                {
                    animationHandler.IdleJump();
                }
                else if (animationHandler.IdleBool == false)
                {
                    animationHandler.RunningJumping();
                }
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

                //animation för falla igenom
                if (animationHandler.IdleBool == true)
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.down), out hit, Mathf.Infinity))
                    {
                        if (hit.transform.gameObject.layer == groundLayer)
                        {
                            animationHandler.IdleFalling();
                        }
                    }
                }
                else if (animationHandler.IdleBool == false)
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.down), out hit, Mathf.Infinity))
                    {
                        if (hit.transform.gameObject.layer == groundLayer)
                        {
                            animationHandler.RunningFalling();
                        }
                    }
                }
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
