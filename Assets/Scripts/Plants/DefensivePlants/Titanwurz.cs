using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Titanwurz : DefensivePlant
{
    public float EffectDuration = 1.0f;
    public float EffectDamage = 1.0f;
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
            List<Enemy> enemiesNotPoisoned = new(gameManager.Enemies);
            enemiesNotPoisoned.ToList().ForEach((enemy) =>
            {
                if (enemy.StatusEffects.OfType<Poisoned>().Any())
                {
                    enemiesNotPoisoned.Remove(enemy);
                }
            });

            if (enemiesNotPoisoned != null)
            {
                Enemy nearestEnemy = this.GetClosestObject(enemiesNotPoisoned);
                if (nearestEnemy && this.GetDistance(nearestEnemy) <= AttackRange)
                {
                    Poisoned poisoned = new(nearestEnemy, EffectDuration, EffectDamage, ParticleEffect);
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
