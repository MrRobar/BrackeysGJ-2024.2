using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    private int maxHealth = 100;

    public event Action<int> OnHealthChanged;

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Destruct();
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

    public void Destruct()
    {
        //Player death
    }
}