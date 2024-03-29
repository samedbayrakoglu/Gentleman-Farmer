using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Transform cropParent;
    [SerializeField] private MeshRenderer tileRenderer;
    private Crop crop;
    private CropData cropData;

    [Header(" Settings")]
    public TileFieldState state;

    [Header(" Events ")]
    public static Action<CropType> OnCropHarvested;



    private void Start()
    {
        state = TileFieldState.Empty;
    }

    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;

        crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);

        this.cropData = cropData;
    }

    public void Water()
    {
        state = TileFieldState.Watered;

        crop.ScaleUp();

        tileRenderer.gameObject.LeanColor(Color.white * 0.3f, 1);
    }

    public void Harvest()
    {
        state = TileFieldState.Empty;

        crop.ScaleDown();

        tileRenderer.gameObject.LeanColor(Color.white, 1);

        OnCropHarvested?.Invoke(cropData.cropType);
    }

    public bool IsEmpty ()
    {
        return state == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }








    // IEnumerator ColorTileCoroutine ()
    // {
    //     float duration = 1;
    //     float timer = 0;

    //     while(timer < duration)
    //     {
    //         float t = timer / duration;

    //         tileRenderer.material.color = Color.Lerp(Color.white, Color.white * 0.3f, t);

    //         timer += Time.deltaTime;

    //         yield return null;
    //     }

    //     yield return null;
    // }
}
