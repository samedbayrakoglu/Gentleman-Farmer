using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileFieldState { Empty, Sown, Watered}

public class CropTile : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Transform cropParent;

    [Header(" Settings")]
    public TileFieldState state;


    private void Start()
    {
        state = TileFieldState.Empty;
    }

    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;

        Crop crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);
    }

    public bool IsEmpty ()
    {
        return state == TileFieldState.Empty;
    }
}
