using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Item
{
    public int startAmount = 0;
    private int amount;

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

    public int getAmount() {
        return amount;
    }

    public void AddHarvest() {
        this.amount += 2;
    }
}
