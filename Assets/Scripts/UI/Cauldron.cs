using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

public class Cauldron : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager GameManager;

    public Outline OutlineValid;
    public Outline OutlineInvalid;

    public TextMeshProUGUI ingredientText1;
    public TextMeshProUGUI ingredientText2;
    public TextMeshProUGUI ingredientText3;
    public TextMeshProUGUI ingredientAmount1;
    public TextMeshProUGUI ingredientAmount2;
    public TextMeshProUGUI ingredientAmount3;

    public ItemAmounts[] ItemAmounts;

    public void UpdateIngredients(InventoryDrops inventoryDrops)
    {
        var index = 0;
        ingredientText1.text = string.Format("{0}:", ItemAmounts[index].Item1.DisplayName);
        ingredientText2.text = string.Format("{0}:", ItemAmounts[index].Item2.DisplayName);
        ingredientText3.text = string.Format("{0}:", ItemAmounts[index].Item3.DisplayName);
        ingredientAmount1.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item1), ItemAmounts[index].Amount1);
        ingredientAmount2.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item2), ItemAmounts[index].Amount2);
        ingredientAmount3.text = string.Format("{0}/{1}", inventoryDrops.GetAmount(ItemAmounts[index].Item3), ItemAmounts[index].Amount3);
    }

    private bool GetValidState()
    {
        var index = 0;
        if (GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[index].Item1) >= ItemAmounts[index].Amount1 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[index].Item2) >= ItemAmounts[index].Amount2 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[index].Item3) >= ItemAmounts[index].Amount3)
        {
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetValidState())
        {
            OutlineValid.Enable();
        }
        else
        {
            OutlineInvalid.Enable();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetValidState())
        {
            OutlineValid.Disable();
        }
        else
        {
            OutlineInvalid.Disable();
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
    }
}
