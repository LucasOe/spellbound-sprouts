using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private GameObject plant;
    [SerializeField] public float maxHealth = 10.0f;
    private float currentHealth;
    
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
}
