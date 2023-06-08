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

    public void Spawn(int day)
    {
        GameManager.CreateEnemy(Skelton, transform.position, transform.rotation);
    }
}