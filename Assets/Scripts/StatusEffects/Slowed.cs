using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowed : StatusEffect
{
    private readonly float strength;

    public Slowed(Enemy enemy, float duration, float strength) : base(enemy, duration)
    {
        this.strength = strength;

        enemy.Agent.speed *= 1 / strength;
    }

    protected override void EndEffect()
    {
        enemy.Agent.speed *= strength;
    }
}
