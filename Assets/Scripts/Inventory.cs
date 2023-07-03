using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //Tools
    public Tool Harvest;
    public Tool Wand;
    public Tool PlantSeeds;
    public Tool HerbSeeds;
    public Tool activeTool;

    public Plant[] plants;
    public Plant[] herbs;
    public Seed[] seeds;
    public int plantIndex;
    public int herbIndex;
    public Plant activePlant = null;
    public Plant activeHerb = null;

    public InventoryDrops InventoryDrops;

    public UI ui;

    public void SetTool(Tool activeTool)
    {
        this.activeTool = activeTool;
    }

    public void SetSeed(int i)
    {
        if (activeTool == PlantSeeds)
        {
            plantIndex = i;
            this.activePlant = plants[i];
        }
        else if (activeTool == HerbSeeds)
        {
            herbIndex = i;
            this.activeHerb = herbs[i];
        }
        ui.selectActiveSeed(i, activeTool);
    }

    public void ScrollSeed(int i)
    {
        if (activeTool == PlantSeeds)
        {
            if (plantIndex + i >= 0 && plantIndex + i < plants.Length)
            {
                plantIndex = plantIndex + i;
                this.activePlant = plants[plantIndex];
                ui.selectActiveSeed(plantIndex, activeTool);
            }
        }
        else if (activeTool == HerbSeeds)
        {
            if (herbIndex + i >= 0 && herbIndex + i < plants.Length)
            {
                herbIndex = herbIndex + i;
                this.activeHerb = herbs[herbIndex];
                ui.selectActiveSeed(herbIndex, activeTool);
            }
        }
    }

    public Plant GetPlant()
    {
        if (activeTool == PlantSeeds)
        {
            return activePlant;
        }
        else
        {
            return activeHerb;
        }
    }

    public void ResetInventory()
    {
        for (int i = 0; i < seeds.Length; i++)
        {
            seeds[i].SetStartAmount();
        }
        plantIndex = 0;
        herbIndex = 0;
    }

}
