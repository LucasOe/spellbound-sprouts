using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowed : StatusEffect
{
    private readonly float strength;

    public Slowed(Enemy enemy, float duration, float strength) : base(enemy, duration)
    {
        this.strength = strength;
    }

    protected override void OnStart()
    {
        enemy.Agent.speed *= 1 / strength;
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnEnd()
    {
        enemy.Agent.speed *= strength;
    }
}
