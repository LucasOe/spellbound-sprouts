using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public ActiveItem activeItem = ActiveItem.Plant;
    [SerializeField] Plant[] _plants;
    [SerializeField] Plant[] _herbs;
    public Plant activePlant = null;
    public Plant activeHerb = null;


    void Start()
    {
        
    }

    public enum ActiveItem {
        Harvest,
        Attack,
        Plant,
        Herb
    }

    public void SetItem(ActiveItem activeItem) {
        this.activeItem = activeItem;
    }

    public void SetItemIndex(int i) {
        this.activePlant = _plants[i];
        this.activeHerb = _herbs[i];
    }

    public Plant GetPlant() {
        if(activeItem == ActiveItem.Plant) {
            return activePlant;
        } else {
            return activeHerb;
        }
    }
}
