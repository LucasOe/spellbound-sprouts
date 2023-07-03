using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum Tool
    {
        Plant,
        Herb,
        Harvest,
    }

    public Plant[] Plants;
    public Plant[] Herbs;
    private int plantIndex;
    private int herbIndex;
    public Tool ActiveTool = Tool.Plant;
    public Plant ActivePlant = null;
    public Plant ActiveHerb = null;

    public InventoryDrops InventoryDrops;
    public UI UI;

    public void Start()
    {
        ActivePlant = Plants[0];
        ActiveHerb = Herbs[0];
    }

    public void SetTool(Tool activeTool)
    {
        this.ActiveTool = activeTool;
    }

    public void SetSeed(int index)
    {
        if (ActiveTool == Tool.Plant)
        {
            plantIndex = index;
            this.ActivePlant = Plants[index];
        }
        else if (ActiveTool == Tool.Herb)
        {
            herbIndex = index;
            this.ActiveHerb = Herbs[index];
        }
        UI.SelectActiveSeed(index, ActiveTool);
    }

    public void ScrollSeed(int index)
    {
        if (ActiveTool == Tool.Plant)
        {
            if (plantIndex + index >= 0 && plantIndex + index < Plants.Length)
            {
                plantIndex += index;
                this.ActivePlant = Plants[plantIndex];
                UI.SelectActiveSeed(plantIndex, ActiveTool);
            }
        }
        else if (ActiveTool == Tool.Herb)
        {
            if (herbIndex + index >= 0 && herbIndex + index < Plants.Length)
            {
                herbIndex += index;
                this.ActiveHerb = Herbs[herbIndex];
                UI.SelectActiveSeed(herbIndex, ActiveTool);
            }
        }
    }

    public Plant GetPlant()
    {
        return ActiveTool == Tool.Plant ? ActivePlant : ActiveHerb;
    }
}
