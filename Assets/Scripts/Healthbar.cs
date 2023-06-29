using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image HealthbarSprite;
    private Quaternion cameraRotation;

    private void Start()
    {
        cameraRotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        // Set Healthbar to face camera
        transform.rotation = cameraRotation;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        HealthbarSprite.fillAmount = currentHealth / maxHealth;
    }
}
