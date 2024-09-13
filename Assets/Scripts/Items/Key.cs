using UnityEngine;

public class Key : AbstractItem
{
    public override void Collect(Inventory inventory)
    {
        base.Collect(inventory);
        Debug.Log("Collected key...");
    }
}