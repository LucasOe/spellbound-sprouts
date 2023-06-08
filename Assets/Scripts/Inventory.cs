using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public ActiveItem activeItem = ActiveItem.Plant;
    public Plant[] plants;
    public Plant[] herbs;
    public Plant activePlant = null;
    public Plant activeHerb = null;

    public enum ActiveItem
    {
        Harvest,
        Attack,
        Plant,
        Herb
    }

    public void SetItem(ActiveItem activeItem)
    {
        this.activeItem = activeItem;
    }

    public void SetItemIndex(int i)
    {
        this.activePlant = plants[i];
        this.activeHerb = herbs[i];
    }

    public Plant GetPlant()
    {
        if (activeItem == ActiveItem.Plant)
        {
            return activePlant;
        }
        else
        {
            return activeHerb;
        }
    }
}
