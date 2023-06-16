using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected GameManager gameManager;

    public Healthbar healthbar;
    public float maxHealth = 10.0f;
    private float currentHealth;

    public Outline outline;
    public NavMeshAgent agent;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // Subscribe to events
        gameManager.CreatedPlant += OnCreatedPlant;
        gameManager.DestroyedPlant += OnDestroyedPlant;

        if (gameManager.Plants.Count >= 0)
        {
            TargetClosestPlant();
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Destroy(GameManager gameManager)
    {
        // Unsubscribe from events
        gameManager.CreatedPlant -= OnCreatedPlant;
        gameManager.DestroyedPlant -= OnDestroyedPlant;
    }

    private void Update()
    {
        // Target player if no plants exist
        if (gameManager.Plants.Count <= 0)
        {
            agent.SetDestination(gameManager.Player.transform.position);
        }
    }

    private void OnCreatedPlant(Plant plant)
    {
        TargetClosestPlant();
    }

    private void OnDestroyedPlant(Plant plant)
    {
        TargetClosestPlant();
    }

    private void TargetClosestPlant()
    {
        Plant closestPlant = this.GetClosestObject(gameManager.Plants);
        if (closestPlant)
        {
            agent.SetDestination(closestPlant.transform.position);
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            gameManager.DestroyEnemy(this);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.Enable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.Disable();
    }
}
