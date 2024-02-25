using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerHarvestAbility : MonoBehaviour
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
        
        CropField.OnFullyHarvested += CropFieldFullyHarvestedCallback;

        playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }

    private void CropFieldFullyHarvestedCallback(CropField cropField)
    {
        if(cropField == currentCropField)
        {
            playerAnimator.StopHarvestAnimation();
        }
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if(!playerToolSelector.CanHarvest())
            playerAnimator.StopHarvestAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            currentCropField = other.GetComponent<CropField>(); // get the crop field to harvest

            EnteredCropField(currentCropField);
        }
    }

    private void EnteredCropField(CropField enteredCropField)
    {
        if(playerToolSelector.CanHarvest())
            playerAnimator.PlayHarvestAnimation();
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            currentCropField = other.GetComponent<CropField>();

            EnteredCropField(currentCropField);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.StopHarvestAnimation();

            currentCropField = null;
        }
    }

    private void OnDestroy()
    {
        CropField.OnFullyHarvested -= CropFieldFullyHarvestedCallback;

        playerToolSelector.OnToolSelected -= ToolSelectedCallback;
    }
}

