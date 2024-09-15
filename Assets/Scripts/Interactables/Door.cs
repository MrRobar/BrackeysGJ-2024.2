using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact(Player player)
    {
        OnInteract?.Invoke();
    }

    public void InteractWith(AbstractItem abstractItem, Player player)
    {
        OnInteract.Invoke();
    }
}