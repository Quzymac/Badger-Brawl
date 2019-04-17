using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace Player
{
    public class AnimationHandler : MonoBehaviour //behöver fler animationer, vapen pickup och så
    {
        Animator anim;
        PlayerScript playerScript;
        PlayerController playerController;
        JumpScript jumpScript;
        Rigidbody rb;
        public bool IdleBool { get; set; }
        public bool Jumping { get; set; }

        private void Start()
        {
            anim = GetComponent<Animator>();
            playerScript = GetComponent<PlayerScript>();
            playerController = GetComponent<PlayerController>();
            jumpScript = GetComponent<JumpScript>();
            rb = GetComponent<Rigidbody>();
        }

        public void IdleJump() //Animation från idle till jump
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Jumping", true);
        }

        public void IdleRun() //amination från idle till running
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Running", true);
        }

        public void IdleFalling() //animation från idle till falling
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", true);
        }

        public void RunningIdle() // animation från running till idle
        {
            anim.SetBool("Running", false);
            anim.SetBool("Idle", true);
        }

        public void RunningJumping() //animation från running till jumping
        {
            anim.SetBool("Running", false);
            anim.SetBool("Jumping", true);
        }

        public void JumpingFalling() //animation från jumping till falling
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }

        public void FallingLanding() //animation från falling till landing
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Landing", true);
        }

        public void LandingIdle() //animation från landing till idle
        {
            anim.SetBool("Landing", false);
            anim.SetBool("Idle", true);
        }

        public void RunningFalling() //animation från running till falling
        {
            anim.SetBool("Running", false);
            anim.SetBool("Falling", true);
        }

        public void LandingRunning()
        {
            anim.SetBool("Landing", false);
            anim.SetBool("Running", true);
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



        //Animationer för att hålla i vapen börjar här. 1H = 1 hand. 2H = 2 händer.

        //Pickup
        public void PickupIdleHold()
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleHold", true);
        }

        public void IdleDrop()
        {
            anim.SetBool("IdleHold", false);
            anim.SetBool("Idle", true);
        }

        //Idle
        public void IdleHoldRun()
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
        public void RunningHold()
        {
            anim.SetBool("idleHold", false);
            anim.SetBool("RunningHold", true);
        }

        //Jumping
        public void JumpingHoldIdle()
        {
            anim.SetBool("IdleHold", false);
            anim.SetBool("jumpingHold", true);
        }

        public void JumpingHoldRunning()
        {
            anim.SetBool("RunningHold", false);
            anim.SetBool("JumpingHold", true);
        }


        //public void JumpingDrop()
        //{
        //    anim.SetBool("JumpingHold", false);
        //    anim.SetBool("Jumping", true);
        //}

        //Falling
        public void FallingHold()
        {
            anim.SetBool("JumpingHold", false);
            anim.SetBool("FallingHold", true);
        }

        public void FallingDrop()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("Falling", true);
        }

        //Landing
        public void LandingFallingHold()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("LandingHold", true);
        }

        public void LandingRunningHold()
        {
            anim.SetBool("FallingHold", false);
            anim.SetBool("RunningHold", true);
        }

        public void LandingIdleHold()
        {
            anim.SetBool("LandingHold", false);
            anim.SetBool("IdleHold", true);
        }

        //Metoder för att kolla vilken animation den ska spela
        public void PickUp() // kolla om spelaren rör på sig eller står stilla när denna plockar upp vapen
        {
            if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1 || IdleBool == false)
            {
                PickupRunningHold();
            }
            else if (rb.velocity.x <= 0.1 || rb.velocity.x >= -0.1)
            {
                PickupIdleHold();
            }
        }

        public void DropWeapon() //kolla om man rör på sig ellelr står stilla för att
        {
            if (rb.velocity.x > 0.1 || rb.velocity.x < -0.1 || IdleBool == false)
            {
                RunningDrop();
            }
            else if (rb.velocity.x <= 0.1 || rb.velocity.x >= -0.1)
            {
                IdleDrop();
            }
        }
    }
}
