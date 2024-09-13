using UnityEngine;

public interface ICollectable 
{
    public Sprite Icon { get; }
    public void Collect(Inventory inventory);
}