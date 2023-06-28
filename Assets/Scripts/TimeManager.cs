using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManger : MonoBehaviour
{
    public GameManager GameManager;
    public float cooldownTime = 60.0f;
    public TextMeshProUGUI timerText;
    private Timer timer;

    private void Start()
    {
        // Subscribe to events
        GameManager.DestroyedEnemy += OnEnemyDeath;

        timer = Timer.CreateTimer(this.gameObject, cooldownTime, () =>
        {
            StartNight(GameManager.Day);
        });
    }

    private void Update()
    {
        if (!GameManager.IsNight && timer.IsRunning)
        {
            timerText.text = timer.DisplayTime();
        }
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

    private void OnEnemyDeath(Enemy enemy)
    {
        // Start next day
        if (GameManager.IsNight && GameManager.Enemies.Count <= 0)
        {
            GameManager.Day += 1;
            StartDay(GameManager.Day);
        }
    }
}
