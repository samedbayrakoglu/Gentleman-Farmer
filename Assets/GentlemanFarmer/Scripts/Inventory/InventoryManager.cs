using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;



    private void Start() 
    {
        inventory = new Inventory();

        CropTile.OnCropHarvested += CropHarvestedCallback;
    }

    private void CropHarvestedCallback(CropType cropType)
    {
        inventory.CropHarvestedCallback(cropType);
    }

    private void OnDestroy()
    {
        CropTile.OnCropHarvested -= CropHarvestedCallback;
    }
}
