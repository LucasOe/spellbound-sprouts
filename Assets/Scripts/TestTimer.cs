using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    public GameManager gameManager;

    public float cooldownTime = 5.0f;
    public Spawner[] spawners;

    private float cooldown;

    private void Start()
    {
        cooldown = cooldownTime;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = cooldownTime; // Reset cooldown
            Execute(gameManager.timeCycle);
        }
    }

    private void Execute(int timeCycle)
    {
        Debug.Log("Spawn Wave: " + timeCycle);
        foreach (Spawner spawner in spawners)
        {
            spawner.Spawn(timeCycle);
        }

        // Set Time
        if (gameManager.timeCycle % 2 == 1)
        {
            gameManager.dayCycleController.StartDay();
            gameManager.DayStart.Invoke(timeCycle);
        }
        else
        {
            gameManager.dayCycleController.StartNight();
            gameManager.NightStart.Invoke(timeCycle);
        }
    }
}
