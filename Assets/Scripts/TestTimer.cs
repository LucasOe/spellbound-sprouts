using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    public float cooldownTime = 5.0f;
    public Spawner[] spawners;

    private float cooldown;
    private int day = 0;

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

            Debug.Log("Spawn Wave: " + day);
            foreach (Spawner spawner in spawners)
            {
                spawner.Spawn(day);
            }

            day++;
        }
    }
}
