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
            ChangeTime(GameManager.Day);
        }
    }

    private void ChangeTime(int day)
    {
        // Note: Currently the first day isn't counted, the first Action that
        // will be invoked is NightStart(0).
        if (GameManager.IsNight)
        {
            Debug.Log("Starting Day: " + day);
            GameManager.DayStart.Invoke(day);
            GameManager.IsNight = false;

            GameManager.Day += 1;
        }
        else
        {
            Debug.Log("Starting Night: " + day);
            GameManager.NightStart.Invoke(day);
            GameManager.IsNight = true;

        }
    }
}
