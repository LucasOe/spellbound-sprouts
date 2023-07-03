using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Item : MonoBehaviour
{
    protected GameManager gameManager;
    public ItemData ItemData;
    public float Dropchance = 1.0f;
    public NavMeshAgent Agent;
    public AudioClip CollectEffect;


    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Update()
    {
        float dist = this.GetDistance(gameManager.Player);
        if (1f < dist && dist < 5f)
        {
            Agent.SetDestination(gameManager.Player.transform.position);
        }
        else if (dist < 3f)
        {
            gameManager.DestroyItem(this);
        }
    }

    public void Destroy(GameManager gameManager)
    {
        gameManager.Player.PlaySound(CollectEffect, 0.3f);
        gameManager.Player.InventoryDrops.AddItem(ItemData, 1); // Add item to Inventory
    }

}
