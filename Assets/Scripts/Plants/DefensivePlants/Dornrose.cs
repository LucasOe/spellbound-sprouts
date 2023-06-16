using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dornrose : DefensivePlant
{
    public float damage = 4.0f;
    public float range = 2.0f;

    void Update()
    {
        Enemy closestEnemy = this.GetClosestObject(gameManager.Enemies);
        if (closestEnemy)
        {
            closestEnemy.Damage(damage);
        }
    }
}
