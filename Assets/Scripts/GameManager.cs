using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;

    // Enemies
    public Action<Enemy> CreatedEnemy;
    public Action<Enemy> DestroyedEnemy;
    public List<Enemy> Enemies;

    // Plants
    public Action<Plant> CreatedPlant;
    public Action<Plant> DestroyedPlant;
    public List<Plant> Plants;

    public Enemy CreateEnemy(Enemy enemy, Vector3 position, Quaternion rotation)
    {
        Enemy spawnedEnemy = Instantiate(enemy, position, rotation);
        spawnedEnemy.Setup(this);
        Enemies.Add(spawnedEnemy);
        CreatedEnemy?.Invoke(spawnedEnemy);
        return spawnedEnemy;
    }

    public void DestroyEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.Destroy(this);
        DestroyedEnemy?.Invoke(enemy);
        Destroy(enemy.gameObject);
    }

    public Plant CreatePlant(Plant plant, Vector3 position, Quaternion rotation)
    {
        Plant spawnedPlant = Instantiate(plant, position, rotation);
        spawnedPlant.Setup(this);
        Plants.Add(spawnedPlant);
        CreatedPlant?.Invoke(spawnedPlant);
        return spawnedPlant;
    }

    public void DestroyPlant(Plant plant)
    {
        Plants.Remove(plant);
        DestroyedPlant?.Invoke(plant);
        Destroy(plant.gameObject);
    }
}
