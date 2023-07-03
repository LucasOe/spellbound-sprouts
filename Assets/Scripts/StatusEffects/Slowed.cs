using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowed : StatusEffect
{
    public Slowed(Enemy enemy, float duration, float strength) : base(enemy, duration)
    {
        Debug.Log("Slowed Start");
    }

    protected override void EndEffect()
    {
        Debug.Log("Slowed End");
    }
}
