using UnityEngine.Events;

public interface IInteractable
{
    public UnityEvent OnInteract { get; }
    public string InteractionText { get; }
    public void Interact(Player player);
    public void InteractWith(AbstractItem abstractItem, Player player);
}