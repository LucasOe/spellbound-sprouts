using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class DefensivePlant : Plant
{
    public GameManager gameManager;
    public float damage = 4.0f;
    public float range; 
    public bool dealsAOE;
    public Enemy focusedEnemy;
    
    public DefensivePlant(GameObject plant) : base(plant)
    {
    }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SetClosestEnemy(gameManager.Enemies);
        if(focusedEnemy) {
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

        this.focusedEnemy = bestTarget;
    }
}
