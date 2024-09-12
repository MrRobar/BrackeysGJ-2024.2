using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractItem : MonoBehaviour, IInteractable, ICollectable
{
    [SerializeField] private UnityEvent OnInteractEvent;
    [SerializeField] private Sprite icon;
    public UnityEvent OnInteract => this.OnInteractEvent;
    public string InteractionText { get; }

    public Sprite Icon => icon;

    public Sprite GetIcon()
    {
        return Icon;
    }

    public void Interact()
    {
        Debug.Log($"Interacting with {name}");
        Collect();
        OnInteract.Invoke();
    }

    public virtual void InteractWith(AbstractItem abstractItem)
    {
    }

    public virtual void Collect()
    {
    }

    public virtual void Drop()
    {
    }
}