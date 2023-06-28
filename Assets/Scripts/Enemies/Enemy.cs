using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected GameManager gameManager;

    public Healthbar Healthbar;
    public float MaxHealth = 10.0f;
    private float currentHealth;
    public float AttackRange = 1.0f;

    public Outline Outline;
    public NavMeshAgent Agent;
    public Animator Animator;

    public AudioSource AudioSource;
    public AudioClip AttackAudioClip;

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
        currentHealth = MaxHealth;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);
    }

    public void Destroy(GameManager gameManager)
    {
        // Unsubscribe from events
        gameManager.CreatedPlant -= OnCreatedPlant;
        gameManager.DestroyedPlant -= OnDestroyedPlant;
    }

    private void Update()
    {
        Animator.SetBool("isWalking", Agent.velocity.magnitude > 0);

        // Target player if no plants exist
        if (gameManager.Plants.Count <= 0)
        {
            if (Utils.GetDistance(this.gameObject, gameManager.Player.gameObject) > AttackRange)
            {
                Agent.SetDestination(gameManager.Player.transform.position);
                Animator.SetBool("isAttacking", false);
            }
            else
            {
                Animator.SetBool("isAttacking", true);
            }
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
            Agent.SetDestination(closestPlant.transform.position);
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);

        if (currentHealth <= 0)
        {
            gameManager.DestroyEnemy(this);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Outline.Enable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Outline.Disable();
    }

    public void AttackEvent()
    {
        AudioSource.PlayOneShot(AttackAudioClip);
    }
}
