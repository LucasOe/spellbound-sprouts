using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected Enemy enemy;

    public StatusEffect(Enemy enemy, float duration)
    {
        this.enemy = enemy;
        enemy.StatusEffects.Add(this);
        Timer timer = enemy.CreateTimer(duration);
        timer.RunOnFinish((state) =>
        {
            enemy.StatusEffects.Remove(this);
            EndEffect();
        });
        timer.StartTimer();
    }

    protected abstract void EndEffect();
}
