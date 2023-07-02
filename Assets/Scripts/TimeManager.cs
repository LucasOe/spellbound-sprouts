using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManger : MonoBehaviour
{
    public GameManager GameManager;
    public float cooldownTime = 60.0f;
    public Timer timer;

    private void Start()
    {
        // Subscribe to events
        GameManager.DestroyedEnemy += OnEnemyDeath;
        GameManager.Spawners.ForEach((spawner) => spawner.SpawnerFinished += OnSpawnerFinished);

        timer = Timer.CreateTimer(this.gameObject, cooldownTime, () =>
        {
            StartNight(GameManager.Day);
        });
    }

    public void StartNight(int day)
    {
        Debug.Log("Starting Night: " + day);
        GameManager.NightStart.Invoke(day);
        GameManager.IsNight = true;
    }

    public void StartDay(int day)
    {
        Debug.Log("Starting Day: " + day);
        GameManager.DayStart.Invoke(day);
        GameManager.IsNight = false;

        timer = Timer.CreateTimer(this.gameObject, cooldownTime, () =>
        {
            StartNight(GameManager.Day);
        });
    }

    public void SkipDay()
    {
        timer.SkipTimer();
    }

    private void NextDay()
    {
        if (GameManager.Enemies.Count <= 0 && FinishedSpawning())
        {
            GameManager.Day += 1;
            StartDay(GameManager.Day);
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        NextDay();
    }

    private void OnSpawnerFinished()
    {
        NextDay();
    }

    private bool FinishedSpawning()
    {
        foreach (Spawner spawner in GameManager.Spawners)
        {
            if (!spawner.FinishedSpawning)
                return false;
        }
        return true;
    }
}
