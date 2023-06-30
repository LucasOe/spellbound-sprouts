using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemDrop : MonoBehaviour
{
    protected GameManager gameManager;
    public Item item;
    public float _dropchance = 0f;

    public GameObject obj = null;
    public NavMeshAgent Agent;

    public AudioClip CollectEffect;


    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // Update is called once per frame
    void Update() 
    {
        if(obj) {
            float dist = this.GetDistance(gameManager.Player);
            if (1f < dist && dist < 5f) 
            {
                Agent.SetDestination(gameManager.Player.transform.position);
            } 
            else if (dist < 3f) 
            {
                gameManager.DestroyItemDrop(this);
            }
        }
    }
    public void Destroy(GameManager gameManager)
    {
        gameManager.Player.PlaySound(CollectEffect, 0.3f);
        item.amount++;
    }
    
}
