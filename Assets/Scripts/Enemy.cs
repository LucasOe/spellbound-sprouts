using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Healthbar healthbar;
    [SerializeField] public float maxHealth = 10.0f;
    private float currentHealth;

    [SerializeField] private Outline outline;

    public GameObject player; // Set by Spanwer script on spawn
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

    public void OnPointerEnter(PointerEventData eventData) {
        outline.Enable();
    }

    public void OnPointerExit(PointerEventData eventData) {
        outline.Disable();
    }
}
