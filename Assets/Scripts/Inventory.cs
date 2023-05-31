using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public ActiveItem activeItem = ActiveItem.Plant;
    [SerializeField] GameObject[] _plants;
    [SerializeField] GameObject[] _herbs;
    public GameObject activePlant = null;
    public GameObject activeHerb = null;


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

    public GameObject getPlant() {
        if(activeItem == ActiveItem.Plant) {
            return activePlant;
        }
        else {
            return activeHerb;
        }
    }
}
