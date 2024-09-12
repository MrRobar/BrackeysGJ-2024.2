using UnityEngine;
using UnityEngine.Events;

public class Bed : IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact()
    {
        Debug.Log("Laying on the bed...");
        OnInteract?.Invoke();
    }

    public void InteractWith(AbstractItem abstractItem)
    {
        Interact();
    }
}