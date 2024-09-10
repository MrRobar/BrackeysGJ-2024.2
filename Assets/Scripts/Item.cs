using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent OnInteractEvent;
    public UnityEvent OnInteract => this.OnInteractEvent;

    public void Interact()
    {
        Debug.Log($"Interacting with {name}");
        OnInteract.Invoke();
    }

    public void InteractWith(Item item)
    {
        throw new System.NotImplementedException();
    }
}