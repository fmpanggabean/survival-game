using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage; 

    public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            fillImage.fillAmount = currentHealth / maxHealth;
        }
}
