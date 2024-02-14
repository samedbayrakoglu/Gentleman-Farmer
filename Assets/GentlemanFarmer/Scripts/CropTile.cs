using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileFieldState { Empty, Sown, Watered}

public class CropTile : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Transform cropParent;
    [SerializeField] private MeshRenderer tileRenderer;
    private Crop crop;

    [Header(" Settings")]
    public TileFieldState state;


    private void Start()
    {
        state = TileFieldState.Empty;
    }

    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;

        crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);
    }

    public void Water()
    {
        state = TileFieldState.Watered;

        tileRenderer.material.color = Color.white * 0.3f; // == (1, 1, 1) * 0.3f 

        crop.ScaleUp();
    }

    public bool IsEmpty ()
    {
        return state == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }
}
