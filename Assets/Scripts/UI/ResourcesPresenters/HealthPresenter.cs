using UnityEngine;
using TMPro;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }
}