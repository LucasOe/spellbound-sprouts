using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantHealthbar : Healthbar
{
    public Image GrowthSprite;

    public void UpdateGrowth(float percent)
    {
        GrowthSprite.fillAmount = percent;
    }
}
