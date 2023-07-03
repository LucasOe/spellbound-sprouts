using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cauldron : MonoBehaviour
{
    public TextMeshProUGUI ingredient1;
    public TextMeshProUGUI ingredient2;
    public TextMeshProUGUI ingredient3;

    public void UpdateIngredients(InventoryDrops inventoryDrops)
    {
        ingredient1.text = string.Format("{0}/3", inventoryDrops.boneAmount);
        ingredient2.text = string.Format("{0}/4", inventoryDrops.spidereyeAmount);
        ingredient3.text = string.Format("{0}/5", inventoryDrops.glassAmount);
    }
}
