using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDrops : MonoBehaviour
{
    public GameManager GameManager;

    public int boneAmount;
    public int spidereyeAmount;
    public int glassAmount;

    private void Awake()
    {
        GameManager.CauldronCanvas.UpdateIngredients(this);
    }

    public void AddItem(Item.Type type)
    {
        _ = type switch
        {
            Item.Type.Bone => boneAmount += 1,
            Item.Type.Eye => spidereyeAmount += 1,
            Item.Type.Glass => glassAmount += 1,
            _ => throw new System.NotImplementedException(),
        };
        GameManager.CauldronCanvas.UpdateIngredients(this);
    }
}
