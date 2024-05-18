using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxStamina = 500; 
    public int currentStamina; 
    public int staminaConsumptionRate = 1; 

    public int staminaRecoverRate = 1;
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    //public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina; 
        staminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20);
        }
        staminaBar.SetStamina(currentStamina);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

        public void HealHP(int hp)
    {
        currentHealth += hp;

        healthBar.SetHealth(currentHealth);
    }

    public void RecoverHP()
    {

        currentHealth += InventoryManager.GetAddHpAmount();
        healthBar.SetHealth(currentHealth);
    }

        public void RecoverStamina(int amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        staminaBar.SetStamina(currentStamina);
    }

    public int GetHP()
    {
        return currentHealth;
    }

    
}
