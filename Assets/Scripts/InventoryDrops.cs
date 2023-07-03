using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemAmount
{
    public ItemData ItemData;
    public int amount;
}

public class InventoryDrops : MonoBehaviour
{
    public GameManager GameManager;
    public ItemAmount[] ItemAmounts;

    private void Awake()
    {
        GameManager.CauldronCanvas.UpdateIngredients(this);
    }

    public void AddItem(ItemData item, int amount)
    {
        ItemAmounts.ForEach((_item) =>
        {
            if (_item.ItemData == item)
                _item.amount += amount;
        });
        GameManager.Player.ui.RefreshAmounts();
        GameManager.CauldronCanvas.UpdateIngredients(this);
    }

    public void RemoveItem(ItemData item, int amount)
    {
        ItemAmounts.ForEach((_item) =>
        {
            if (_item.ItemData == item)
                _item.amount -= amount;
        });
        GameManager.Player.ui.RefreshAmounts();
        GameManager.CauldronCanvas.UpdateIngredients(this);
    }

    public int GetAmount(ItemData item)
    {
        var amount = 0;
        ItemAmounts.ForEach((_item) =>
        {
            if (_item.ItemData == item)
                amount = _item.amount;
        });
        return amount;
    }

    public int GetAmount(int index)
    {
        return ItemAmounts[index].amount;
    }
}
