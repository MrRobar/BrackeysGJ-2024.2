using UnityEngine;

public interface ICollectable 
{
    public Sprite Icon { get; }
    public Sprite GetIcon();
    public void Collect();
}