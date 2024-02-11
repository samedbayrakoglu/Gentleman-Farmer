using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform tilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();



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
        tileToSow.Sow();
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
}
