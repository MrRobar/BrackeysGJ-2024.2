using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI healthText;
    private void OnEnable()
    {
        player.OnHealthChanged += UpdateText;
    }

    private void OnDisable()
    {
        player.OnHealthChanged -= UpdateText;
    }

    private void UpdateText(int health)
    {
        healthText.text = health.ToString();
    }
}