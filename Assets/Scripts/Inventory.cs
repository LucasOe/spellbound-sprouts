using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public enum ActiveItem {
        Harvest,
        Attack,
        Roses,
        Burdock,
        Flytrap,
        Titan,
        Herb1,
        Herb2,
        Herb3
    }

    public void SetItem(ActiveItem activeItem) {}

}
