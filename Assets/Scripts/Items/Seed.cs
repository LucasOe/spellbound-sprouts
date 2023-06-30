using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public int _startAmount = 2;
    public int _amount;

    void Start()
    {
        this._amount = _startAmount;
    }

    public void SetStartAmount()
    {
        this._amount = _startAmount;
    }

    public void AddHarvest(bool mature)
    {
        this._amount += mature ? 2 : 1;
    }

    public void PlantSeed()
    {
        this._amount -= 1;
    }
}
