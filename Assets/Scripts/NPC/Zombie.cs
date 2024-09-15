using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private PlayerDetector detector;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackCooldown = 2f;
    
    private NavMeshAgent navMeshAgent;
    private Player player;
    private float timeSinceLastAttack;
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        detector.SetRadius(visionRange);
        detector.OnPlayerDetected += SetPlayer;
        detector.OnPlayerLost += ResetPlayer;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        
        if (player == null) 
            return;
        
        var toPlayer = player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, toPlayer) <= fieldOfView/2)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            if(timeSinceLastAttack >= attackCooldown)
            {
                player.ReceiveDamage(attackDamage);
                timeSinceLastAttack = 0f;
            }
        }
    }

    private void OnDestroy()
    {
        detector.OnPlayerDetected -= SetPlayer;
        detector.OnPlayerLost -= ResetPlayer;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
    
    public void ResetPlayer()
    {
        player = null;
    }
    
    
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"Zombie {transform.name} has been killed");
        // Implement death logic here
    }
}
