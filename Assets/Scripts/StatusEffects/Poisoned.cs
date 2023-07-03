using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : StatusEffect
{
    public Poisoned(Enemy enemy, float duration, float cooldown, float damage) : base(enemy, duration)
    {
        Debug.Log("Poisoned Start");
    }

    protected override void EndEffect()
    {
        Debug.Log("Poisoned End");
    }

}
