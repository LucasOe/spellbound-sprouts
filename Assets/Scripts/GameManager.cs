using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;

    public Action<Enemy> CreatedEnemy;
    public Action<Enemy> DestroyedEnemy;
    public List<Enemy> Enemies;

    public Enemy CreateEnemy(Enemy enemy, Vector3 position, Quaternion rotation) {
        Enemy spawnedEnemy = Instantiate(enemy, position, rotation);
        Enemies.Add(spawnedEnemy);
        CreatedEnemy?.Invoke(spawnedEnemy);
        return spawnedEnemy;
    }

    public void DestroyEnemy(Enemy enemy) {
        Enemies.Remove(enemy);
        DestroyedEnemy?.Invoke(enemy);
        Destroy(enemy);
    }
}
