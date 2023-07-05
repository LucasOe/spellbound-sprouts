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
    public Item ItemDrop;
    public int ItemDropAmount;

    public int age = 0;
    public bool mature;
    public GameObject[] Stages;

    public AudioClip HarvestClip;
    public AudioClip PlantDamageClip;

    public int drops = 2;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
        // Subscribe to events
        gameManager.DayStart += OnDayStart;
        gameManager.NightStart += OnNightStart;
    }

    public void Destroy(GameManager gameManager)
    {
        // Unsubscribe from events
        gameManager.DayStart -= OnDayStart;

        if (mature)
        {
            for (int i = 0; i < ItemDropAmount; i++)
            {
                gameManager.CreateItem(ItemDrop, transform.position);
            }
        }
        else
        {
            gameManager.CreateItem(ItemDrop, transform.position);
        }

        //Sound
        gameManager.Player.PlaySound(HarvestClip);
    }

    protected virtual void Start()
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

        //Sound
        gameManager.Player.PlaySound(HarvestClip);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);

        //Sound
        gameManager.Player.PlaySound(PlantDamageClip, .1f);


        if (currentHealth <= 0)
        {
            gameManager.DestroyPlant(this);
        }
    }

    protected virtual void OnDayStart(int day)
    {
        this.currentHealth += MaxHealth * .5f;
        this.currentHealth = (currentHealth > MaxHealth) ? MaxHealth : currentHealth;
        if (Stages.Length == 1)
            mature = true;

        // Grow plants
        if (!mature)
        {
            Stages[age].SetActive(false);
            age++;
            Stages[age].SetActive(true);
            ActiveStage = Stages[age];

            // Plant reached max age
            if (age >= Stages.Length - 1)
                mature = true;
        }

        SetHealthbarActive(false);

        var growthPercent = (float)age / (Stages.Length - 1);
        Healthbar.UpdateGrowth(growthPercent);
        Healthbar.UpdateHealthBar(currentHealth, MaxHealth);
    }

    protected virtual void OnNightStart(int day)
    {
        SetHealthbarActive(true);
    }

    public void SetHealthbarActive(bool value)
    {
        Healthbar.gameObject.SetActive(value);
    }
}
