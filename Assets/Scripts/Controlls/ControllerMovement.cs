using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

namespace Player {
    public class ControllerMovement : MonoBehaviour
    {

        [SerializeField] float moveSpeed = 8f;

        bool lookingRight = true;
        Rigidbody rb;
        public float MoveDir { get; set; }

        float speed;
        float moveForce = 50;

        JumpScript jumpScript;
        AnimationHandler animationHandler;
        Animator animator;
        [SerializeField] AnimationClip running;

        private void Start()
        {
            jumpScript = GetComponent<JumpScript>();
            animationHandler = GetComponent<AnimationHandler>();
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            speed = moveSpeed;
        }

        private void Update()
        {
            if (jumpScript.Running)
            {
                animationHandler.IdleToRun();
            }
            else
            {
                animationHandler.RunToIdle();
            }
        }

        private void FixedUpdate()
        {
            //rotate to match movement direction
            if (MoveDir < 0 && lookingRight)
            {
                lookingRight = false;
            }
            else if (MoveDir > 0 && !lookingRight)
            {
                lookingRight = true;
            }

            //extra friction when not moving to prevent player from moving when not getting inputs.
            if (MoveDir == 0)
            {
                GetComponent<Collider>().material.dynamicFriction = 5f;
            }
            else if (MoveDir != 0 && GetComponent<Collider>().material.dynamicFriction != 0)
            {
                GetComponent<Collider>().material.dynamicFriction = 0f;
            }


            if (MoveDir * rb.velocity.x < speed)
            {
                rb.AddForce(Vector2.right * MoveDir * moveForce);

            }
   
            if (Mathf.Abs(rb.velocity.x) > speed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
            }
        }
        public void KnockBack(Vector3 origin, float power)
        {
            Vector3 dir = (transform.position - origin).normalized;
            rb.AddForce(dir * power, ForceMode.Impulse);
        }

        public float MovementReturn()
        {
            float movementReturn;
            movementReturn = moveSpeed;
            return movementReturn;
        }

    }
}
