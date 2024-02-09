using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Joystick joystick;
    private PlayerAnimator playerAnimator;
    private CharacterController characterController;

    [Header(" Settings")]
    [SerializeField] private float moveSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        ManageMovement();
    }

    private void ManageMovement () 
    {
        Vector3 movementVec = joystick.GetMoveVector() * moveSpeed * Time.deltaTime / Screen.width;

        movementVec.z = movementVec.y; // to swap the input Y value with Z value
        movementVec.y = 0;

        characterController.Move(movementVec);

        playerAnimator.ManageAnimations(movementVec);
    }
}
