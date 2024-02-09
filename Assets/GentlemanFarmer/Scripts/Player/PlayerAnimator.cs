using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Animator animator;

    [Header(" Settings")]
    [SerializeField] private float moveSpeedMultiplier;


    public void ManageAnimations (Vector3 moveVec)
    {
        if(moveVec.magnitude > 0) 
        {
            animator.SetFloat("moveSpeed", moveVec.magnitude * moveSpeedMultiplier);

            PlayRunAnimation();

            animator.transform.forward = moveVec.normalized; // animator.transform = playerModel.transform
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayRunAnimation ()
    {
        animator.Play("Run");
    }

    private void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }
}
