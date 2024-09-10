using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        Debug.Log($"Zombie {transform.name} has been killed");
        // Implement death logic here
    }
}