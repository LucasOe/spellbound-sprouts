using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Tool activeTool = Plant;
    public Plant[] plants;
    public Plant[] herbs;
    public Plant activePlant = null;
    public Plant activeHerb = null;

    //Tools
    public Tool Harvest = new Tool();
    public Tool Staff = new Tool();
    public Tool PlantSeeds = new Tool();
    public Tool HerbSeeds = new Tool();

    public void SetTool(Tool activeTool)
    {
        this.activeTool = activeTool;
    }

    public void SetItemIndex(int i)
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
