using UnityEngine;

public class Package : AbstractItem
{
    public override void Collect(Inventory inventory)
    {
        base.Collect(inventory);
        Debug.Log("Picked up package...");
    }
}