using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractItem : MonoBehaviour, IInteractable, ICollectable
{
    [SerializeField] private UnityEvent OnInteractEvent;
    [SerializeField] private Sprite icon;
    public UnityEvent OnInteract => OnInteractEvent;
    public string InteractionText { get; }
    public bool InInventory { get; private set; }
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
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = true;
        }
        if (TryGetComponent<Collider>(out var collider))
        {
            collider.enabled = false;
        }
        if (TryGetComponent<Animator>(out var animator))
        {
            animator.enabled = false;
        }
        enabled = true;
        InInventory = true;
        inventory.AddItem(this);
        OnInteract.Invoke();
    }

    public virtual void Drop()
    {
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = false;
        }
        if (TryGetComponent<Collider>(out var collider))
        {
            collider.enabled = true;
        }
        if (TryGetComponent<Animator>(out var animator))
        {
            animator.enabled = true;
        }
        enabled = true;
        InInventory = false;
        transform.position = playerT.position + playerT.forward * 0.5f;
    }
}