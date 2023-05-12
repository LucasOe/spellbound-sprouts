using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] public float maxHealth = 10.0f;
    private float currentHealth;

    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;

    private void Start() {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update() {
        agent.SetDestination(player.transform.position);
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if(currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void Heal(float amount) {
        currentHealth += amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
