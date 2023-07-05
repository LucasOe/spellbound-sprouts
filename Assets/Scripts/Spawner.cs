using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct EnemySpawnInfo
{
    public int skeletonCount;
    public int spiderCount;
}

public class Spawner : MonoBehaviour
{
    public GameManager GameManager;
    public Enemy Skeleton;
    public Enemy Spider;
    public float SpawnCooldown = 5.0f;
    public EnemySpawnInfo[] EnemySpawnInfo;
    public Action SpawnerFinished;
    public bool FinishedSpawning;

    void Start()
    {
        GameManager.NightStart += OnNightStart;
    }

    public void OnNightStart(int day)
    {
        FinishedSpawning = false;
        if (day <= EnemySpawnInfo.Length)
        {
            // Spawn Enemy
            if (EnemySpawnInfo[day].skeletonCount > 0 || EnemySpawnInfo[day].spiderCount > 0)
            {
                Timer timer = this.CreateTimer(SpawnCooldown, GetEnemyCount(day) - 1);
                timer.RunOnFinish((state) =>
                {
                    SpawnEnemies(day);
                });
                timer.StartTimer();
                // Skip cooldown so the first wave spawns instantly
                timer.SkipTimer();
            }
            else
            {
                FinishedSpawning = true;
            }
        }
        else
        {
            Debug.Log("No spawn info for this day");
            SceneManager.LoadScene(2);
        }
    }

    private void SpawnEnemies(int day)
    {
        var spiderCount = EnemySpawnInfo[day].spiderCount;
        var skeletonCount = EnemySpawnInfo[day].skeletonCount;

        if (spiderCount <= 0 && skeletonCount > 0)
        {
            SpawnSkeleton(day);
        }
        else if (spiderCount > 0 && skeletonCount <= 0)
        {
            SpawnSpider(day);
        }
        else if (spiderCount > 0 && skeletonCount > 0)
        {
            if (UnityEngine.Random.value >= 0.5f)
                SpawnSkeleton(day);
            else
                SpawnSpider(day);
        }
        else
        {
            FinishedSpawning = true;
            SpawnerFinished?.Invoke();
        }
    }

    private void SpawnSkeleton(int day)
    {
        GameManager.CreateEnemy(Skeleton, transform.position, transform.rotation);
        EnemySpawnInfo[day].skeletonCount--;
        TestFinished(day);
    }

    private void SpawnSpider(int day)
    {
        GameManager.CreateEnemy(Spider, transform.position, transform.rotation);
        EnemySpawnInfo[day].spiderCount--;
        TestFinished(day);
    }

    private void TestFinished(int day)
    {
        if (EnemySpawnInfo[day].skeletonCount <= 0 && EnemySpawnInfo[day].spiderCount <= 0)
        {
            FinishedSpawning = true;
            SpawnerFinished?.Invoke();
        }
    }

    public int GetEnemyCount(int day)
    {
        return EnemySpawnInfo[day].skeletonCount + EnemySpawnInfo[day].spiderCount;
    }
}