using UnityEngine;
using UnityEngine.Events;

public class Bed : IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact(Player player)
    {
        Debug.Log("Laying on the bed...");
        OnInteract?.Invoke();
    }

    public void InteractWith(AbstractItem abstractItem, Player player)
    {
        Interact(player);
    }
}