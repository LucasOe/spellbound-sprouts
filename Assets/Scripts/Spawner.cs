using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnemySpawnInfo {
    public int skeletonCount;
    public int spiderCount;
}

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject skelton;
    public GameObject spider;
    public EnemySpawnInfo[] days;

    public void Spawn(int day) {
        GameObject enemyGameObject = Instantiate(skelton, transform.position, transform.rotation);
        if(enemyGameObject.TryGetComponent(out Enemy enemy)) {
            enemy.player = player;
        }
    }
}