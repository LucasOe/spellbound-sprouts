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

    public ItemData Reward;
    public int RewardAmount;
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

    private int currentState = 0;

    public void UpdateIngredients()
    {
        ingredientText1.text = string.Format("{0}:", ItemAmounts[currentState].Item1.DisplayName);
        ingredientText2.text = string.Format("{0}:", ItemAmounts[currentState].Item2.DisplayName);
        ingredientText3.text = string.Format("{0}:", ItemAmounts[currentState].Item3.DisplayName);
        ingredientAmount1.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item1), ItemAmounts[currentState].Amount1);
        ingredientAmount2.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item2), ItemAmounts[currentState].Amount2);
        ingredientAmount3.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item3), ItemAmounts[currentState].Amount3);
    }

    private bool GetValidState()
    {
        return
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item1) >= ItemAmounts[currentState].Amount1 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item2) >= ItemAmounts[currentState].Amount2 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item3) >= ItemAmounts[currentState].Amount3;
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
        if (GetValidState())
        {
            currentState += 1;
            GameManager.Player.InventoryDrops.AddItem(ItemAmounts[currentState].Reward, ItemAmounts[currentState].RewardAmount);
            UpdateIngredients();
        }
    }
}
