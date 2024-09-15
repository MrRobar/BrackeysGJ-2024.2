using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public Inventory Inventory => inventory;
    [SerializeField] private Inventory inventory;
    [SerializeField] private int maxHealth = 100;
    private int health;
    
    

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        OnHealthChanged?.Invoke(health);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        OnHealthChanged?.Invoke(health);
    }

    public void Die()
    {
        //Player death
    }
}
