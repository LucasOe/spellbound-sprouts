using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    private ActiveItem activeItem = ActiveItem.Harvest;
    [SerializeField] GameObject _plant;
    [SerializeField] GameObject _herb;


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

    public GameObject getPlant() {
        if(activeItem == ActiveItem.Plant) {
            return _plant;
        }
        else {
            return _herb;
        }
    }
}
