using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip swing;
    public int damage = 10;
    public float attackRange = 1.5f;

    private bool canAttack = true;
    public float attackCooldown = 1f; // Время между атаками

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        AudioSystem.Instance.PlaySoundOnce(swing, transform);
        canAttack = false;

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Zombie>()?.ReceiveDamage(damage); // Убедитесь, что враги имеют компонент "EnemyHealth"
        }

        Invoke(nameof(ResetAttack), attackCooldown); // Перезарядка атаки
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}