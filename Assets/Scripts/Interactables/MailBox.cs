using UnityEngine;
using UnityEngine.Events;

public class MailBox : IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact()
    {
        Debug.Log("Need to put a package in here first...");
    }

    public void InteractWith(AbstractItem abstractItem)
    {
        Debug.Log("Putted a package in here...");
        OnInteract?.Invoke();
    }
}
