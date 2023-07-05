using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Klette : DefensivePlant
{
    public float EffectDuration = 5.0f;
    public float EffectStrength = 1.0f;
    public float AttackDamage = 2.0f;
    public float AttackRange = 2.0f;
    public float AttackSpeed = 0.2f;
    public GameObject ParticleEffect;

    private Timer attackTimer;

    protected override void Start()
    {
        base.Start();

        attackTimer = this.CreateTimer(AttackSpeed, -1);
        attackTimer.RunOnFinish((state) =>
        {
            List<Enemy> enemiesNotSlowed = new(gameManager.Enemies);
            enemiesNotSlowed.ToList().ForEach((enemy) =>
            {
                if (enemy.StatusEffects.OfType<Slowed>().Any())
                {
                    enemiesNotSlowed.Remove(enemy);
                }
            });

            if (enemiesNotSlowed != null)
            {
                Enemy nearestEnemy = this.GetClosestObject(enemiesNotSlowed);
                if (nearestEnemy && this.GetDistance(nearestEnemy) <= AttackRange)
                {
                    Slowed slowed = new(nearestEnemy, EffectDuration, EffectStrength, ParticleEffect);
                }
            }
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
