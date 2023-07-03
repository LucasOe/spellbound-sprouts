using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Klette : DefensivePlant
{
    public float EffectDuration = 5.0f;
    public float EffectStrength = 1.0f;
    public float AttackRange = 2.0f;
    public float AttackSpeed = 0.2f;

    private Timer attackTimer;

    protected override void Start()
    {
        base.Start();

        attackTimer = this.CreateTimer(AttackSpeed, -1);
        attackTimer.RunOnFinish((state) =>
        {
            gameManager.Enemies.ToList().ForEach((enemy) =>
            {
                if (this.GetDistance(enemy) <= AttackRange && enemy.StatusEffects.Count <= 0)
                {
                    Slowed slowed = new(enemy, EffectDuration, EffectStrength);
                }
            });
        });
    }

    protected override void OnNightStart(int day)
    {
        base.OnNightStart(day);

        if (mature)
            attackTimer.StartTimer();
    }

    protected override void OnDayStart(int day)
    {
        base.OnDayStart(day);

        if (attackTimer.IsRunning())
            attackTimer.StopTimer();
    }
}
