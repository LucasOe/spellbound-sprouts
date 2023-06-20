using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected GameManager gameManager;

    public Healthbar healthbar;
    public GameObject plant;
    public float maxHealth = 10.0f;
    private float currentHealth;

    private int age = 0;
    private bool mature;
    public GameObject[] stages;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // Subscribe to events
        gameManager.DayStart += OnDayStart;
    }

    public void Destroy(GameManager gameManager)
    {
        // Unsubscribe from events
        gameManager.DayStart -= OnDayStart;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDayStart(int day)
    {
        // Grow plants
        if (!mature && stages.Length >= 1)
        {
            stages[age].SetActive(false);
            age++;
            stages[age].SetActive(true);
            plant = stages[age];

            // Plant reached max age
            if (age >= stages.Length - 1)
            {
                mature = true;
            }
        }

    }
}
