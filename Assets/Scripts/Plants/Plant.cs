using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    protected GameManager gameManager;

    public PlantHealthbar Healthbar;
    public GameObject ActiveStage;
    public float MaxHealth = 10.0f;
    private float currentHealth;
    public Seed seed;

    private int age = 0;
    private bool mature;
    public GameObject[] Stages;

    public int drops = 2;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // Subscribe to events
        gameManager.DayStart += OnDayStart;
    }

    public void Destroy(GameManager gameManager)
    {
        seed.AddHarvest();
        // Unsubscribe from events
        gameManager.DayStart -= OnDayStart;
    }

    private void Start()
    {
        currentHealth = MaxHealth;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);
        Healthbar.UpdateGrowth(0);
        SetHealthbarActive(false);

        // Set first growth stage active
        Stages.ForEach(stage => stage.SetActive(false));
        Stages[0].SetActive(true);
        ActiveStage = Stages[0];

        // Random Rotation
        var rotation = Random.Range(0.0f, 360.0f);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnDayStart(int day)
    {
        // Grow plants
        if (!mature && Stages.Length >= 1)
        {
            Stages[age].SetActive(false);
            age++;
            Stages[age].SetActive(true);
            ActiveStage = Stages[age];

            // Plant reached max age
            if (age >= Stages.Length - 1)
            {
                mature = true;
            }
        }

        var growthPercent = (float)age / (Stages.Length - 1);
        Healthbar.UpdateGrowth(growthPercent);
    }

    public void SetHealthbarActive(bool value)
    {
        Healthbar.gameObject.SetActive(value);
    }
}
