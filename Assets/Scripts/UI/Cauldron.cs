using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct ItemAmounts
{
    public ItemData Item1;
    public int Amount1;

    public ItemData Item2;
    public int Amount2;

    public ItemData Item3;
    public int Amount3;
}

public class Cauldron : MonoBehaviour
{
    public TextMeshProUGUI ingredient1;
    public TextMeshProUGUI ingredient2;
    public TextMeshProUGUI ingredient3;

    public ItemAmounts[] ItemAmounts;

    public void UpdateIngredients(InventoryDrops inventoryDrops)
    {
        var index = 0;
        ingredient1.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item1), ItemAmounts[index].Amount1);
        ingredient2.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item2), ItemAmounts[index].Amount2);
        ingredient3.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item3), ItemAmounts[index].Amount3);
    }
}
