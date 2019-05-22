using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{
    [SerializeField] Transform rightTarget;

    [SerializeField] Transform leftTarget;

    public Transform LeftHand { get { return leftTarget; } set { leftTarget = value; } }
    public Transform RightHand { get { return rightTarget; } set { rightTarget = value; } }


    [SerializeField] float strenghtPosition = 0.6f;
    [SerializeField] float strenghtRotation = 0.6f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK()
    {
        if (rightTarget != null)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, strenghtPosition);

            animator.SetIKRotation(AvatarIKGoal.RightHand, rightTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, strenghtRotation);
        }

        if (leftTarget != null)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, strenghtPosition);

            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftTarget.rotation);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, strenghtRotation);
        }
    }
}
