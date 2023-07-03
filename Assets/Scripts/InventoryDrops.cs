using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDrops : MonoBehaviour
{
    public int boneAmount;
    public int spidereyeAmount;
    public int glassAmount;

    public void AddItem(Item.Type type)
    {
        _ = type switch
        {
            Item.Type.Bone => boneAmount += 1,
            Item.Type.Eye => spidereyeAmount += 1,
            Item.Type.Glass => glassAmount += 1,
            _ => throw new System.NotImplementedException(),
        };
    }
}
