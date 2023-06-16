using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    public GameManager GameManager;

    public float cooldownTime = 5.0f;
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
            Execute(GameManager.timeCycle);
        }
    }

    private void Execute(int timeCycle)
    {
        Debug.Log("Spawn Wave: " + timeCycle);

        // Set Time
        if (GameManager.timeCycle % 2 == 1)
        {
            GameManager.DayStart.Invoke(timeCycle);
        }
        else
        {
            GameManager.NightStart.Invoke(timeCycle);
        }

        GameManager.timeCycle += 1;
    }
}
