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
    public Enemy Skelton;
    public Enemy Spider;
    //public EnemySpawnInfo[] Days;

    void Start()
    {
        GameManager.NightStart += OnNightStart;
    }

    public void OnNightStart(int day)
    {
        if (Random.value >= 0.5)
        {
            GameManager.CreateEnemy(Skelton, transform.position, transform.rotation);
        }
        else
        {
            GameManager.CreateEnemy(Spider, transform.position, transform.rotation);
        }
    }
}