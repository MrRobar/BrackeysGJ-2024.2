using UnityEngine.Events;

public interface IInteractable
{
    public UnityEvent OnInteract { get; }
    public void Interact();
    public void InteractWith(Item item);
}