using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact(Player player)
    {
        Debug.Log("Opening door...");
        OnInteract?.Invoke();
    }

    public void InteractWith(AbstractItem abstractItem, Player player)
    {
        Debug.Log("Opening door with item...");
        Debug.Log(abstractItem.name);
        OnInteract.Invoke();
    }
}