using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractItem : MonoBehaviour, IInteractable, ICollectable
{
    [SerializeField] private UnityEvent OnInteractEvent;
    [SerializeField] private Sprite icon;
    public UnityEvent OnInteract => OnInteractEvent;
    public string InteractionText { get; }
    private Transform playerT;

    public Sprite Icon => icon;

    public void Interact(Player player)
    {
        Debug.Log($"Interacting with {name}");
        playerT = player.transform;
        Collect(player.Inventory);
    }

    public virtual void InteractWith(AbstractItem abstractItem, Player player)
    {
        player.Inventory.DropCurrentItem();
        Collect(player.Inventory);
    }

    public virtual void Collect(Inventory inventory)
    {
        inventory.AddItem(this);
        OnInteract.Invoke();
    }

    public virtual void Drop()
    {
        gameObject.SetActive(true);
        transform.position = playerT.position + playerT.forward * 0.5f;
    }
}