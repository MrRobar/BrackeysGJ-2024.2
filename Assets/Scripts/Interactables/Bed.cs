using UnityEngine;
using UnityEngine.Events;

public class Bed : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }
    [SerializeField] private SimpleRule rule;

    public void Interact(Player player)
    {
        if (Work.Orders.Length == 0)
        {
            rule.Complete();
            OnInteract?.Invoke();
        }
    }

    public void InteractWith(AbstractItem abstractItem, Player player)
    {
        Interact(player);
    }
}