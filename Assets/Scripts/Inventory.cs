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
    public Plant activePlant = null;
    public Plant activeHerb = null;
    
    public void SetTool(Tool activeTool)
    {
        this.activeTool = activeTool;
    }

    public void SetSeed(int i)
    {
        if(activeTool == PlantSeeds) 
        {
            this.activePlant = plants[i];
        }
        else if (activeTool == HerbSeeds) 
        {
            this.activeHerb = herbs[i];
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
}
