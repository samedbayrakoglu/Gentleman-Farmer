using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Inventory
{
    [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();


    public void CropHarvestedCallback(CropType cropType)
    {
        bool cropFound = false;

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            InventoryItem item = inventoryItems[i];

            if(item.cropType == cropType)
            {
                item.amount++;

                cropFound = true;

                break;
            }
        }
        
        DebugInventory();

        if(cropFound)
            return;

        // create new crop item
        inventoryItems.Add(new InventoryItem(cropType, 1));

    }

    private void DebugInventory()
    {
        foreach(InventoryItem item in inventoryItems)
        {
            Debug.Log("We have "+ item.amount + " items in our " + item.cropType + " list.");
        }
    }
}
