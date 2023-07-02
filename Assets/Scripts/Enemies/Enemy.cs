using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected GameManager gameManager;

    public Healthbar Healthbar;
    private float currentHealth;
    public float MaxHealth = 10.0f;
    public float AttackRange = 1.0f;
    public float AttackDamage = 1.0f;
    public ItemDrop[] ItemDrops;

    public Outline Outline;
    public NavMeshAgent Agent;
    public Animator Animator;

    public AudioSource AudioSource;
    public AudioClip AttackAudioClip;

    public MonoBehaviour currentTarget;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        currentHealth = MaxHealth;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);
    }

    public void Destroy(GameManager gameManager)
    {

    }

    private void Update()
    {
        Animator.SetBool("isWalking", Agent.velocity.magnitude > 0);

        // Target player if no plants exist
        if (gameManager.Plants.Count <= 0)
        {
            if (this.GetDistance(gameManager.Player) > AttackRange)
                TargetGameObject(gameManager.Player);
            else
                AttackGameObject(gameManager.Player);
        }
        else
        {
            Plant closestPlant = this.GetClosestObject(gameManager.Plants);
            if (this.GetDistance(closestPlant) > AttackRange)
                TargetGameObject(closestPlant);
            else
                AttackGameObject(closestPlant);
        }
    }

    private void TargetGameObject(MonoBehaviour target)
    {
        currentTarget = target;
        Agent.SetDestination(target.transform.position);
        Animator.SetBool("isAttacking", false);
    }

    private void AttackGameObject(MonoBehaviour target)
    {
        currentTarget = target;
        Agent.SetDestination(this.transform.position);
        Animator.SetBool("isAttacking", true);
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
        if (currentTarget.TryGetComponent(out Plant plant))
        {
            AudioSource.PlayOneShot(AttackAudioClip);
            plant.Damage(AttackDamage);
        }
    }
}
