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
    public bool IsFinished = true;

    void Start()
    {
        GameManager.NightStart += OnNightStart;
    }

    public void OnNightStart(int day)
    {
        IsFinished = false;
        if (day <= EnemySpawnInfo.Length)
        {
            // Spawn Skeleton
            if (EnemySpawnInfo[day].skeletonCount > 0)
            {
                Timer timer = Timer.CreateTimer(this.gameObject, SpawnCooldown, () =>
                {
                    GameManager.CreateEnemy(Skeleton, transform.position, transform.rotation);
                    EnemySpawnInfo[day].skeletonCount--;
                    if (EnemySpawnInfo[day].skeletonCount <= 0 && EnemySpawnInfo[day].spiderCount <= 0)
                    {
                        IsFinished = true;
                    }
                }, EnemySpawnInfo[day].skeletonCount - 1);
                timer.SkipTimer(); // Skip first cooldown
            }

            // Spawn Spiders
            if (EnemySpawnInfo[day].spiderCount > 0)
            {
                Timer timer = Timer.CreateTimer(this.gameObject, SpawnCooldown, () =>
                {
                    GameManager.CreateEnemy(Spider, transform.position, transform.rotation);
                    EnemySpawnInfo[day].spiderCount--;
                    if (EnemySpawnInfo[day].skeletonCount <= 0 && EnemySpawnInfo[day].spiderCount <= 0)
                    {
                        IsFinished = true;
                    }
                }, EnemySpawnInfo[day].spiderCount - 1);
                timer.SkipTimer(); // Skip first cooldown
            }
        }
        else
        {
            Debug.Log("No spawn info");
        }
    }
}