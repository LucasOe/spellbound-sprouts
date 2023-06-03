using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    private List<Plant> plants;

    [SerializeField] private Healthbar healthbar;
    [SerializeField] public float maxHealth = 10.0f;
    private float currentHealth;

    [SerializeField] private Outline outline;

    [SerializeField] private NavMeshAgent agent;

    public void Setup(GameManager gameManager) {
        this.gameManager = gameManager;
        // Subscribe to events
        gameManager.CreatedPlant += OnCreatedPlant;
        gameManager.DestroyedPlant += OnDestroyedPlant;

        if(gameManager.Plants.Count >= 0) {
            TargetClosestPlant();
        }
    }

    private void Start() {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Destroy(GameManager gameManager) {
        gameManager.CreatedPlant -= OnCreatedPlant;
        gameManager.DestroyedPlant -= OnDestroyedPlant;
    }

    private void Update() {
        // Target player if no plants exist
        if(gameManager.Plants.Count <= 0) {
            agent.SetDestination(gameManager.Player.transform.position);
        }
    }

    private void OnCreatedPlant(Plant plant) {
        TargetClosestPlant();
    }

    private void OnDestroyedPlant(Plant plant) {
        TargetClosestPlant();
    }

    private void TargetClosestPlant() {
        Plant closestPlant = GetClosestPlant(gameManager.Plants);
        if(closestPlant) {
            agent.SetDestination(closestPlant.transform.position);
        }
    }

    private Plant GetClosestPlant(List<Plant> plants) {
        Plant bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Plant potentialTarget in plants) {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr) {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if(currentHealth <= 0) {
            gameManager.DestroyEnemy(this);
        }
    }

    public void Heal(float amount) {
        currentHealth += amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        outline.Enable();
    }

    public void OnPointerExit(PointerEventData eventData) {
        outline.Disable();
    }
}
