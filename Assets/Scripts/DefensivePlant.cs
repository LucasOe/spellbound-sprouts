using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensivePlant : Plant
{
    public GameManager gameManager;
    public float damage = 4.0f;
    public float range;
    public bool dealsAOE;
    public Enemy focusedEnemy;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        SetClosestEnemy(gameManager.Enemies);
        if (focusedEnemy)
        {
            focusedEnemy.Damage(damage);
        }
    }

    private void SetClosestEnemy(List<Enemy> enemies)
    {
        Enemy bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Enemy potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        focusedEnemy = bestTarget;
    }
}
