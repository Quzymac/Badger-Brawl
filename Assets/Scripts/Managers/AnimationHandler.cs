using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

namespace Player
{
    public class AnimationHandler : MonoBehaviour //behöver fler animationer, vapen pickup och så
    {
        Animator anim;
        PlayerScript playerScript;
        JumpScript jumpScript;
        Rigidbody rb;
        ControllerMovement controllerMovement;
        float moveSpeed;
        public bool IdleBool { get; set; }
        public bool Jumping { get; set; }

        private void Start()
        {
            anim = GetComponent<Animator>();
            playerScript = GetComponent<PlayerScript>();
            jumpScript = GetComponent<JumpScript>();
            rb = GetComponent<Rigidbody>();
            controllerMovement = GetComponent<ControllerMovement>();
            moveSpeed = controllerMovement.MovementReturn();
        }

        private void Update()
        {
            TwoAnimationsFix();      
        }

        public void IdleJump() //Animation från idle till jump
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Jumping", true);
            anim.SetTrigger("IdleJumpTrigger");
        }

        public void IdleRun() //amination från idle till running
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
            //anim.SetFloat("Speed", Mathf.Abs(moveSpeed));
            //anim.SetTrigger("IdleRunTrigger");
        }

        public void IdleFalling() //animation från idle till falling
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", true);
            //anim.SetTrigger("IdleFallingTrigger");
        }

        public void RunningIdle() // animation från running till idle
        {
            anim.SetBool("Running", false);
            anim.SetBool("Idle", true);
            //anim.SetFloat("Speed", 0);
            //anim.SetTrigger("RunIdleTrigger");
        }

        public void RunningJumping() //animation från running till jumping
        {
            anim.SetBool("Running", false);
            anim.SetBool("Jumping", true);
            //anim.SetTrigger("RunningJumpingTrigger");
        }

        public void JumpingFalling() //animation från jumping till falling
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
            //anim.SetTrigger("JumpingFallingTrigger");
        }

        public void FallingLanding() //animation från falling till landing
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Landing", true);
            //anim.SetTrigger("LandingTrigger");
        }

        public void LandingIdle() //animation från landing till idle
        {
            anim.SetBool("Landing", false);
            anim.SetBool("Idle", true);
            //anim.SetTrigger("LandingIdleTrigger");
        }

        public void RunningFalling() //animation från running till falling
        {
            anim.SetBool("Running", false);
            anim.SetBool("Falling", true);
            //anim.SetTrigger("RunningFallingTrigger");
        }

        public void LandingRunning()
        {
            anim.SetBool("Landing", false);
            anim.SetBool("Running", true);
            //anim.SetTrigger("LandingRunningTrigger");
        }

        public void FallingRunning()
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Running", true);
        }

        public void FallingIdle()
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Idle", true);
        }

        public void JumpingIdle()
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Idle", true);
        }

        public void JumpingRunning()
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Running", true);
        }

        //Animationer för att hålla i vapen börjar här.

        //Pickup
        public void PickupIdleHold()
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleHold", true);
            //anim.SetTrigger("PickUpTrigger");
        }

        public void IdleDrop()
        {
            anim.SetBool("IdleHold", false);
            anim.SetBool("Idle", true);
        }

        //Idle
        public void RunningIdleHold()
        {
            anim.SetBool("RunningHold", false);
            anim.SetBool("IdleHold", true);
        }

        //RunningPickup
        public void PickupRunningHold()
        {
            anim.SetBool("Running", false);
            anim.SetBool("RunningHold", true);
        }

        public void RunningDrop()
        {
            anim.SetBool("RunningHold", false);
            anim.SetBool("Running", true);
        }

        //Running
        public void IdleRunHold()
        {
            anim.SetBool("IdleHold", false);
            anim.SetBool("RunningHold", true);
            //anim.SetTrigger("IdleRunHoldTrigger");
        }

        //Jumping
        public void IdleJumpHold()
        {
            //anim.SetBool("IdleHold", false);
            //anim.SetBool("JumpingHold", true);
            anim.SetTrigger("IdleJumpHoldTrigger");
        }

        public void RunningJumpingHold()
        {
            //anim.SetBool("RunningHold", false);
            //anim.SetBool("JumpingHold", true);
            anim.SetTrigger("RunningJumpingHoldTrigger");
        }


        //public void JumpingDrop()
        //{
        //    anim.SetBool("JumpingHold", false);
        //    anim.SetBool("Jumping", true);
        //}

        //Falling
        public void JumpingFallingHold()
        {
            //anim.SetBool("JumpingHold", false);
            //anim.SetBool("FallingHold", true);
            anim.SetTrigger("JumpingFallingHoldTrigger");
        }

        public void FallingDrop()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("Falling", true);
        }

        //Landing
        public void FallingLandingHold()
        {
            //anim.SetBool("FallingHold", false);
            //anim.SetBool("LandingHold", true);
            anim.SetTrigger("LandingTriggerHold");
        }

        public void FallingIdleHold()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("IdleHold", true);
        }

        public void FallingRunningHold()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("RunningHold", true);
        }

        public void IdleFallingHold()
        {
            anim.SetBool("IdleHold", false);
            anim.SetBool("FallingHold", true);
        }

        public void RunningFallingHold()
        {
            anim.SetBool("RunningHold", false);
            anim.SetBool("FallingHold", true);
        }

        public void LandingIdleHold()
        {
            //anim.SetBool("LandingHold", false);
            //anim.SetBool("IdleHold", true);
            anim.SetTrigger("LandingIdleHoldTrigger");
        }

        public void LandingRunningHold()
        {
            //anim.SetBool("LandingHold", false);
            //anim.SetBool("RunningHold", true);
            anim.SetTrigger("LandingRunningHoldTrigger");
        }

        public void JumpingIdleHold()
        {
            anim.SetBool("JumpingHold", false);
            anim.SetBool("IdleHold", true);
        }

        public void JumpingRunningHold()
        {
            anim.SetBool("JumpingHold", false);
            anim.SetBool("RunningHold", true);
        }

        //Metoder för att kolla vilken animation den ska spela
        public void PickUp() // kolla om spelaren rör på sig eller står stilla när denna plockar upp vapen
        {
            if (jumpScript.Running == true)
            {
                PickupRunningHold();
            }
            else if (jumpScript.Running == false)
            {
                PickupIdleHold();
            }
        }

        public void DropWeapon() //kolla om man rör på sig eller står stilla för att
        {
            if (jumpScript.Running == true)
            {
                RunningDrop();
            }
            else if (jumpScript.Running == false)
            {
                IdleDrop();
            }
        }

        public void IdleToRun() //kolla om karaktären håller i vapnet när den går från idle till run animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                IdleRunHold();
                Debug.Log("Should play running hold animation");
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                IdleRun();
            }
        }

        public void RunToIdle() //kolla om karaktären håller i vapen när den går från Run till idle animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                RunningIdleHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                RunningIdle();
            }
        }

        public void RunToJump()//kolla om karaktären håller i vapen när den går från Run till jump animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                RunningJumpingHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                RunningJumping();
            }
        }

        public void JumpingToFalling()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                JumpingFallingHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                JumpingFalling();
            }
        }

        public void IdleToJump() //kolla om karaktären håller i vapen när den går från Idle till jump animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                IdleJumpHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                IdleJump();
            }
        }

        public void IdleToFall() //kolla om karaktären håller i vapen när den går från idle till fall animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                IdleFallingHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                IdleFalling();
            }
        }

        public void RunningToFall() //kolla om karaktären håller i vapen när den går från running till falling animationen
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                RunningFallingHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                RunningFalling();
            }
        }

        public void FallingToLanding()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                FallingLandingHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                FallingLanding();
            }
        }

        public void FallingToIdle()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                FallingIdleHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                FallingIdle();
            }
        }

        public void FallingToRunning()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                FallingRunningHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                FallingRunning();
            }
        }

        public void LandingToIdle()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                LandingIdleHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                LandingIdle();
            }
        }

        public void LandingToRunning()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                LandingRunningHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                LandingRunning();
            }
        }

        public void JumpingToIdle()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                JumpingIdleHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                JumpingIdle();
            }
        }

        public void JumpingToRunning()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                JumpingRunningHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                JumpingRunning();
            }
        }

        public void FallingJumpingCheck()
        {
            if (playerScript.GetComponentInChildren<IWeapon>() != null)
            {
                FallingJumpHold();
            }
            else if (playerScript.GetComponentInChildren<IWeapon>() == null)
            {
                FallingJump();
            }
        }

        public void Throwing()
        {
            anim.SetTrigger("Throwing");
        }

        public void JumpToJump()
        {
            anim.SetTrigger("JumpJump");
        }

        public void FallingJump()
        {
            anim.SetTrigger("FallingJumping");
        }

        public void FallingJumpHold()
        {
            anim.SetTrigger("FallingJumpingHold");
        }

        public void TwoAnimationsFix() // ska försöka fixa så flera animationer inte försöker spelas samtidigt
        {
            if (anim.GetBool("Jumping") == true && (anim.GetBool("Jumping") == true || anim.GetBool("IdleHold") == true || anim.GetBool("Falling") == true || anim.GetBool("Running") == true))
            {
                anim.SetBool("Jumping", false);
            }
            else if (anim.GetBool("Idle") == true && (anim.GetBool("Jumping") == true || anim.GetBool("IdleHold") == true || anim.GetBool("Falling") == true || anim.GetBool("Running") == true))
            {
                anim.SetBool("IdleHold", false);
            }
            else if (anim.GetBool("FallingHold") == true && (anim.GetBool("Idle") == true || anim.GetBool("IdleHold") == true || anim.GetBool("Falling") == true || anim.GetBool("Running") == true))
            {
                anim.SetBool("FallingHold", false);
            }
            else if (anim.GetBool("Falling") == true && (anim.GetBool("Idle") == true || anim.GetBool("IdleHold") == true || anim.GetBool("FallingHold") == true))
            {
                anim.SetBool("Falling", false);
            }
            else if (anim.GetBool("IdleHold") == true && (anim.GetBool("Running") == true || anim.GetBool("FallingHold") == true || anim.GetBool("RunningHold") == true))
            {
                anim.SetBool("Running", false);
                anim.SetBool("RunningHold", false);
            }
        }
    }
}
