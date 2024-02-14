using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform tilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();
    
    [Header(" Settings")]
    [SerializeField] private CropData cropData;
    private TileFieldState fieldState;

    private int tilesSown;
    private int tilesWatered;

    [Header(" Actions ")]
    public static Action<CropField> OnFullySown;
    public static Action<CropField> OnFullyWatered;



    private void Start()
    {
        StoreTiles();
    }

    private void StoreTiles ()
    {
        for (int i = 0; i < tilesParent.childCount; i++)
        {
            cropTiles.Add(tilesParent.GetChild(i).GetComponent<CropTile>());
        }
    }

    public void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);

            if (closestCropTile == null) // check if there is a crop tile
                continue;

            if (!closestCropTile.IsEmpty()) // check if the tile is empty
                continue;

            Sow(closestCropTile);
        }
    }

    private void Sow (CropTile tileToSow)
    {
        tileToSow.Sow(cropData);

        tilesSown++;
        if (tilesSown == cropTiles.Count)
            FieldFullySown();
    }

    public void WaterCollidedCallback(Vector3[] waterPositions)
    {
        for (int i = 0; i < waterPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(waterPositions[i]);

            if (closestCropTile == null) // check if there is a crop tile
                continue;

            if (!closestCropTile.IsSown()) // check if the tile is sown
                continue;

            Water(closestCropTile);
        }
    }

    private void Water(CropTile cropTile)
    {
        cropTile.Water();

        tilesWatered++;
        if(tilesWatered == cropTiles.Count)
            FieldFullyWatered();
    }

    private void FieldFullySown ()
    {
        fieldState = TileFieldState.Sown;

        OnFullySown?.Invoke(this);
    }

    private void FieldFullyWatered()
    {
        fieldState = TileFieldState.Watered;

        OnFullyWatered?.Invoke(this);
    }

    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = Mathf.Infinity;

        CropTile closestTile = null;

        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile ct = cropTiles[i];

            float distanceTiletoSeed = Vector3.Distance(ct.transform.position, seedPosition);

            if(distanceTiletoSeed < minDistance)
            {
                minDistance = distanceTiletoSeed;

                closestTile = ct;
            }
        }

        if (closestTile == null)
            return null;

        return closestTile;
    }

    public bool IsEmpty ()
    {
        return fieldState == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return fieldState == TileFieldState.Sown;
    }

    public bool IsWatered()
    {
        return fieldState == TileFieldState.Watered;
    }
}
