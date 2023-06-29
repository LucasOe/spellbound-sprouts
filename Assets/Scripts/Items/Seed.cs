using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public int startAmount = 2;
    public int amount;

    void Start()
    {
        this.amount = startAmount;
    }

    public void SetStartAmount()
    {
        this.amount = startAmount;
    }

    public void AddHarvest(bool mature)
    {
        this.amount += mature ? 2 : 1;
    }

    public void PlantSeed()
    {
        this.amount -= 1;
    }
}
