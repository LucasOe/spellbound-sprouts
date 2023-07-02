using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
                var enemyCount = EnemySpawnInfo[day].skeletonCount + EnemySpawnInfo[day].spiderCount;
                Timer timer = Timer.CreateTimer(this.gameObject, SpawnCooldown, () =>
                {
                    if (EnemySpawnInfo[day].spiderCount <= 0 && EnemySpawnInfo[day].skeletonCount > 0)
                    {
                        SpawnSkeleton(day);
                    }
                    else if (EnemySpawnInfo[day].spiderCount > 0 && EnemySpawnInfo[day].skeletonCount <= 0)
                    {
                        SpawnSpider(day);
                    }
                    else if (EnemySpawnInfo[day].spiderCount > 0 && EnemySpawnInfo[day].skeletonCount > 0)
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
                }, enemyCount - 1);
                timer.SkipTimer(); // Skip first cooldown
            }
        }
        else
        {
            Debug.Log("No spawn info");
        }
    }

    private void SpawnSkeleton(int day)
    {
        GameManager.CreateEnemy(Skeleton, transform.position, transform.rotation);
        EnemySpawnInfo[day].skeletonCount--;

        if (EnemySpawnInfo[day].skeletonCount <= 0 && EnemySpawnInfo[day].spiderCount <= 0)
        {
            FinishedSpawning = true;
            SpawnerFinished?.Invoke();
        }
    }

    private void SpawnSpider(int day)
    {
        GameManager.CreateEnemy(Spider, transform.position, transform.rotation);
        EnemySpawnInfo[day].spiderCount--;

        if (EnemySpawnInfo[day].skeletonCount <= 0 && EnemySpawnInfo[day].spiderCount <= 0)
        {
            FinishedSpawning = true;
            SpawnerFinished?.Invoke();
        }
    }
}