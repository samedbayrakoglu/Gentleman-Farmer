using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToolSelector : MonoBehaviour
{
    public enum Tool { None, Sow, Water, Harvest}
    private Tool activeTool;

    [Header(" Elements ")]
    [SerializeField] private Image[] toolImages;

    [Header(" Settings ")]
    [SerializeField] public Color selectedToolColor;

    [Header(" Actions ")]
    public Action<Tool> OnToolSelected;



    private void Start() 
    {
        SelectTool(0); // select none at start
    }

    public void SelectTool(int toolIndex)
    {
        activeTool = (Tool)toolIndex; // cast the tool index to enum

        for (int i = 0; i < toolImages.Length; i++)
        {
            toolImages[i].color = i == toolIndex ? selectedToolColor : Color.white;
        }

        OnToolSelected?.Invoke(activeTool);
    }

    public bool CanSow()
    {
        return activeTool == Tool.Sow;
    }

    public bool CanWater()
    {
        return activeTool == Tool.Water;
    }

    public bool CanHarvest()
    {
        return activeTool == Tool.Harvest;
    }
}
