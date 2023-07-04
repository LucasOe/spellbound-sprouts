using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : StatusEffect
{
    private readonly float damage;

    public Poisoned(Enemy enemy, float duration, float damage) : base(enemy, duration)
    {
        this.damage = damage;
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        enemy.Damage(damage * Time.deltaTime);
    }

    protected override void OnEnd()
    {

    }
}
