using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerWaterAbility : MonoBehaviour
{
    [Header(" Elements")]
    private PlayerAnimator playerAnimator;
    private PlayerToolSelector playerToolSelector;

    [Header(" Settings")]
    private CropField currentCropField;



    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerToolSelector = GetComponent<PlayerToolSelector>();

        WaterParticles.OnWaterCollided += WaterCollidedCallback;
        
        // CropField.OnFullySown += CropFieldFullySownCallback;

        playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }

    private void WaterCollidedCallback(Vector3[] waterPositions) 
    {
        if (currentCropField == null)
            return;

        // currentCropField.WaterCollidedCallback(waterPositions);
    }

    private void CropFieldFullySownCallback(CropField cropField)
    {
        if(cropField == currentCropField)
        {
            playerAnimator.StopSowAnimation();
        }
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if(!playerToolSelector.CanWater())
            playerAnimator.StopWaterAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            currentCropField = other.GetComponent<CropField>(); // get the crop field to sow

            EnteredCropField(currentCropField);
        }
    }

    private void EnteredCropField(CropField enteredCropField)
    {
        if(playerToolSelector.CanSow())
            playerAnimator.PlaySowAnimation();
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            currentCropField = other.GetComponent<CropField>();

            EnteredCropField(currentCropField);
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
        WaterParticles.OnWaterCollided -= WaterCollidedCallback;

        // CropField.OnFullySown -= CropFieldFullySownCallback;

        playerToolSelector.OnToolSelected -= ToolSelectedCallback;

    }
}
