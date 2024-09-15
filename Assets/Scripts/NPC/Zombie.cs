using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private PlayerDetector detector;
    [SerializeField] private float fieldOfView = 60f;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private Animator animator;

    private NavMeshAgent navMeshAgent;
    private Player player;
    private bool isDead = false;

    public bool IsPlayerVisible;

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
        if (isDead) return;

        if (player == null)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        var toPlayer = player.transform.position - transform.position;
        IsPlayerVisible = Vector3.Angle(transform.forward, toPlayer) <= fieldOfView;
        if (IsPlayerVisible)
        {
            navMeshAgent.SetDestination(player.transform.position);

            if (toPlayer.magnitude > attackRange)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
            }

            return;
        }

        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        detector.OnPlayerDetected -= SetPlayer;
        detector.OnPlayerLost -= ResetPlayer;
    }

    public void Attack()
    {
        if (player != null)
        {
            player.ReceiveDamage(attackDamage);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void ResetPlayer()
    {
        animator.SetBool("isWalking", false);
        player = null;
        navMeshAgent.ResetPath();
    }

    public void ReceiveDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("isHit");
        }
    }

    public void ResetHit()
    {
        animator.ResetTrigger("isHit");
    }

    public void Die()
    {
        isDead = true;
        navMeshAgent.isStopped = true;
        animator.SetBool("isDead", true);
        // Дополнительная логика смерти (например, удаление зомби через время)
    }
}