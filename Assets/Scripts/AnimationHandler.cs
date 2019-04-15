using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace Player
{
    public class AnimationHandler : MonoBehaviour //behöver fler animationer, vapen pickup och så
    {
        Animator anim;
        public bool IdleBool { get; set; }
        public bool Jumping { get; set; }

        private void Start()
        {
            anim = GetComponent<Animator>();
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
    }
}
