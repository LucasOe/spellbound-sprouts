using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private GameObject plant;
    [SerializeField] public float maxHealth = 10.0f;
    private float currentHealth;
    private int growthDuration = 2;
    private int age = 0;
    
    public Plant(GameObject plant) {
        this.plant = plant;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        healthbar.UpdateHealthBar(currentHealth, maxHealth);

        if(currentHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void Grow() {
        this.age++;
        plant.transform.localScale = new Vector3(age*2f + 1f, age*2f + 1f, age*2f + 1f);
    }
}
