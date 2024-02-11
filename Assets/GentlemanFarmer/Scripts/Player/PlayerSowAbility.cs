using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerSowAbility : MonoBehaviour
{
    [Header(" Elements")]
    private PlayerAnimator playerAnimator;

    [Header(" Settings")]
    private CropField currentCropField;




    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        SeedParticles.OnSeedCollied += SeedsCollidedCallback;
    }

    private void SeedsCollidedCallback(Vector3[] seedCollisionPositions) 
    {
        if (currentCropField == null)
            return;

        currentCropField.SeedsCollidedCallback(seedCollisionPositions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CropField"))
        {
            playerAnimator.PlaySowAnimation();

            currentCropField = other.GetComponent<CropField>(); // get the crop field to sow
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.StopSowAnimation();

            currentCropField = null;
        }
    }

    private void OnDestroy()
    {
        SeedParticles.OnSeedCollied -= SeedsCollidedCallback;
    }
}
