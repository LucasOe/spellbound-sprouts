using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public Healthbar healthbar;
    public GameObject plant;
    public float maxHealth = 10.0f;
    private float currentHealth;
    private int age = 0;
    private int maxAge = 3;

    public Plant(GameObject plant)
    {
     this.plant = plant;
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

    public void Grow()
    {
        if(this.age < this.maxAge) {
            this.age++;
            plant.transform.localScale = new Vector3(age * 2f + 1f, age * 2f + 1f, age * 2f + 1f);
        }
    }
}
