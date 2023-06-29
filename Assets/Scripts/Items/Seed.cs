using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public int startAmount = 2;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        this.amount = startAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStartAmount() {
        this.amount = startAmount;
    }

    public void AddHarvest(bool mature) {
        this.amount += mature ?  2 : 1;
    }

    public void PlantSeed() {
        this.amount -= 1;
    }
}
