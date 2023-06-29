using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;

    // Tiles
    public List<Tile> Tiles;

    // Enemies
    public Action<Enemy> CreatedEnemy;
    public Action<Enemy> DestroyedEnemy;
    public List<Enemy> Enemies;

    // Plants
    public Action<Plant> CreatedPlant;
    public Action<Plant> DestroyedPlant;
    public List<Plant> Plants;

    // Seeds
    public List<Seed> Seeds;

    // Day Night Cycle
    public TimeManger TimeManger;
    public DayCycleController DayCycleController;
    public int Day = 0;
    public bool IsNight = false;
    public Action<int> DayStart;
    public Action<int> NightStart;

    public Tile CreateTile(Tile tile, Vector3 position, Quaternion rotation, MonoBehaviour parent = null)
    {
        Tile spawnedTile = Instantiate(tile, position, rotation);
        if (parent)
            spawnedTile.transform.SetParent(parent.transform, false);

        spawnedTile.Setup(this);
        Tiles.Add(spawnedTile);
        return spawnedTile;
    }

    public Enemy CreateEnemy(Enemy enemy, Vector3 position, Quaternion rotation, MonoBehaviour parent = null)
    {
        Enemy spawnedEnemy = Instantiate(enemy, position, rotation);
        if (parent)
            spawnedEnemy.transform.SetParent(parent.transform, true);

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

    public Plant CreatePlant(Plant plant, Vector3 position, Quaternion rotation, MonoBehaviour parent = null)
    {
        Plant spawnedPlant = Instantiate(plant, position, rotation);
        if (parent)
            spawnedPlant.transform.SetParent(parent.transform, true);

        spawnedPlant.Setup(this);
        Plants.Add(spawnedPlant);
        CreatedPlant?.Invoke(spawnedPlant);
        return spawnedPlant;
    }

    public void DestroyPlant(Plant plant)
    {
        Plants.Remove(plant);
        plant.Destroy(this);
        DestroyedPlant?.Invoke(plant);
        Destroy(plant.gameObject);
    }
}
