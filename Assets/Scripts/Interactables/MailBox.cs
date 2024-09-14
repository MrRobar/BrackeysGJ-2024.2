using System;
using UnityEngine;
using UnityEngine.Events;

public class MailBox : IInteractable
{
    public event Action PackageDelivered;
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }

    public void Interact(Player player)
    {
        Debug.Log("Need to put a package in here first...");
    }

    public void InteractWith(AbstractItem abstractItem, Player player)
    {
        if (abstractItem.TryGetComponent(out Package package))
        {
            Debug.Log("Putted a package in here...");
            PackageDelivered?.Invoke();
        }
        
        Debug.Log("Need to put a package in here first...");
        OnInteract?.Invoke();
    }
}
